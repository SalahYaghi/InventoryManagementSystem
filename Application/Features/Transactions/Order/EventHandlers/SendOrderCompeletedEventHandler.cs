using Application.Common.Dtos.Notifications;
using Application.Common.Interfaces;
using Contract.Common.Interfaces;
using Domain.Orders;
using Domain.Orders.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Application.Features.Transactions.Order.EventHandlers
{
    public class SendOrderCompeletedEventHandler(
        IAppDbContext context,
        ILogger<SendOrderCompeletedEventHandler> logger,
        INotificationService notificationService)
        : INotificationHandler<OrderCompeletedEvent>
    {
        public async Task Handle(OrderCompeletedEvent notification, CancellationToken ct)
        {
            var order = await context.Orders
                .Include(o => o.Supplier)
                    .ThenInclude(s => s!.Contact)
                .Include(o => o.Customer)
                    .ThenInclude(c => c!.Contact)
                .Include(o => o.Invoice)
                .Include(o => o.SourceWarehouse)
                .Include(o => o.DestinationWarehouse)
                .Include(o => o.OrderDetails)
                .AsSplitQuery()  // [FIX 4.5-style] several Includes; avoid the cartesian product
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == notification.OrderId, ct);

            if (order is null)
            {
                logger.LogWarning(
                    "Order completed event was received, but order with id {OrderId} was not found.",
                    notification.OrderId);

                return;
            }

            var recipientEmail = GetRecipientEmail(order);
            var recipientPhone = GetRecipientPhone(order);

            if (recipientEmail is null && recipientPhone is null)
            {
                logger.LogInformation(
                    "Order {OrderId} completed; no external counterparty to notify (type {OrderType}).",
                    order.Id, order.OrderType);
                return;
            }

            if (recipientEmail is not null)
            {
                try
                {
                    await notificationService.SendEmailAsync(
                        new EmailMessageDto(To: recipientEmail, Subject: BuildEmailSubject(order), Body: BuildEmailBody(order)), ct);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to send the completion email for order {OrderId}.", order.Id);
                }
            }
            else
            {
                logger.LogWarning("Order {OrderId} completed but the counterparty has no email on file.", order.Id);
            }

            if (recipientPhone is not null)
            {
                try
                {
                    await notificationService.SendSMSAsync(
                        new SmsMessageDto(PhoneNumber: recipientPhone, Message: BuildSmsMessage(order)), ct);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to send the completion SMS for order {OrderId}.", order.Id);
                }
            }
        }

        private static string? GetRecipientEmail(Domain.Orders.Order order)
        {
            var email = order.OrderType switch
            {
                OrderType.Purchase or OrderType.ReturnOut => order.Supplier?.Contact?.Email,
                OrderType.Sale or OrderType.ReturnIn => order.Customer?.Contact?.Email,
                _ => null
            };

            return string.IsNullOrWhiteSpace(email) ? null : email;
        }

        private static string? GetRecipientPhone(Domain.Orders.Order order)
        {
            var phone = order.OrderType switch
            {
                OrderType.Purchase or OrderType.ReturnOut => order.Supplier?.Contact?.PhoneNumber,
                OrderType.Sale or OrderType.ReturnIn => order.Customer?.Contact?.PhoneNumber,
                _ => null
            };

            return string.IsNullOrWhiteSpace(phone) ? null : phone;
        }

        private static string BuildEmailSubject(Domain.Orders.Order order)
        {
            return $"Order Completed - {order.OrderType} Order";
        }

        private static string BuildEmailBody(Domain.Orders.Order order)
        {
            var partyName = GetPartyName(order);
            var sourceWarehouse = order.SourceWarehouse?.Name ?? "Not specified";
            var destinationWarehouse = order.DestinationWarehouse?.Name ?? "Not specified";

            var builder = new StringBuilder();

            builder.AppendLine("Dear User,");
            builder.AppendLine();
            builder.AppendLine("We would like to inform you that an order has been successfully completed.");
            builder.AppendLine();
            builder.AppendLine("Order Summary:");
            builder.AppendLine($"- Order Id: {order.Id}");
            builder.AppendLine($"- Order Type: {order.OrderType}");
            builder.AppendLine($"- Order Status: {order.OrderStatus}");
            builder.AppendLine($"- Related Party: {partyName}");
            builder.AppendLine($"- Source Warehouse: {sourceWarehouse}");

            if (order.OrderType == OrderType.Transfer)
            {
                builder.AppendLine($"- Destination Warehouse: {destinationWarehouse}");
            }

            builder.AppendLine($"- Subtotal Amount: {order.SubTotalAmount:C}");
            builder.AppendLine($"- Discount Amount: {(order.DiscountAmount ?? 0):C}");
            builder.AppendLine($"- Net Amount: {order.NetAmount:C}");
            builder.AppendLine($"- Due Date: {order.DueDate:yyyy-MM-dd HH:mm}");
            builder.AppendLine($"- Invoice Status: {(order.AlreadyHaveInvoice() ? "Invoice issued" : "Invoice not issued yet")}");

            if (!string.IsNullOrWhiteSpace(order.Notes))
            {
                builder.AppendLine();
                builder.AppendLine("Notes:");
                builder.AppendLine(order.Notes);
            }

            builder.AppendLine();
            builder.AppendLine("Order Details:");
            builder.AppendLine($"- Total Items: {order.OrderDetails.Count}");

            foreach (var detail in order.OrderDetails)
            {
                builder.AppendLine($"  • Detail Id: {detail.Id}");
                builder.AppendLine($"    Total Amount: {detail.TotalAmount:C}");
            }

            builder.AppendLine();
            builder.AppendLine("Thank you.");
            builder.AppendLine("Mechanic Shop Management System");

            return builder.ToString();
        }

        private static string BuildSmsMessage(Domain.Orders.Order order)
        {
            var partyName = GetPartyName(order);

            return
                $"Order completed successfully. " +
                $"Type: {order.OrderType}, " +
                $"Party: {partyName}, " +
                $"Net Amount: {order.NetAmount:C}, " +
                $"Due Date: {order.DueDate:yyyy-MM-dd}.";
        }

        private static string GetPartyName(Domain.Orders.Order order)
        {
            return order.OrderType switch
            {
                OrderType.Purchase or OrderType.ReturnOut =>
                    order.Supplier?.SupplierName ?? "Supplier not specified",

                OrderType.Sale or OrderType.ReturnIn =>
                    order.Customer?.CustomerName ?? "Customer not specified",

                OrderType.Transfer =>
                    "Internal warehouse transfer",

                _ =>
                    "Not specified"
            };
        }
    }
}

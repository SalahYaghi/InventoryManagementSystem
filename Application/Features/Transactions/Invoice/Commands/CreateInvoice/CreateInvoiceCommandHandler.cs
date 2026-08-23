using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Invoices;
using Contract.Features.Transactions.Invoice.DTOs;
using Contract.Features.Transactions.Invoice.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using Contract.Common.Errors;
using Domain.Orders;
using MechanicShop.Domain.Common.Constamts;

namespace Contract.Features.Transactions.Invoice.Commands.CreateInvoice
{
    public sealed class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<InvoiceDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateInvoiceCommandHandler> _logger;

        public CreateInvoiceCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateInvoiceCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<InvoiceDto>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateInvoiceCommandHandler));


            var order = await _context.Orders.Where(o => o.Id == request.OrderId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)

            {

                _logger.LogWarning("CreateInvoiceCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.OrderNotFound");
                return ApplicationErrors.OrderNotFound;

            }
            if (order.OrderStatus != OrderStatus.Completed)
            {
                _logger.LogError("CreateInvoiceCommandHandler stopped because an error result was returned: {ErrorResult}.", "OrderErrors.CannotIssueInvoiceBeforeOrderCompeletion");
                return OrderErrors.CannotIssueInvoiceBeforeOrderCompeletion;
            }
            if (!Domain.Orders.Order.CanIssueInvoiceForOrderType(order.OrderType))
            {
                _logger.LogError("CreateInvoiceCommandHandler stopped because an error result was returned: {ErrorResult}.", "OrderErrors.IssueInoiceInvalidForType");
                return OrderErrors.IssueInoiceInvalidForType;
            }
            if (order.AlreadyHaveInvoice())
            {
                _logger.LogWarning("CreateInvoiceCommandHandler stopped: order already has an issued invoice.");
                return OrderErrors.OrderAlreadyHasIssuedLicense;
            }


            Guid invoiceId = Guid.NewGuid();

            List<InvoiceLineItem> LineItems = new List<InvoiceLineItem>();

            foreach (var (detail, lineNo) in order.OrderDetails.Select((o, i) => (o, i + 1)))
            {
                var lineDescription = $"{lineNo}: {detail.Product!.ProductName}";

                var item = InvoiceLineItem.Create(
                    lineNo,
                    invoiceId,
                    lineDescription,
                    InventoryManagementConstants.TaxRate * detail.UnitPrice * detail.Quantity,
                    detail.Quantity,
                    detail.UnitPrice);

                if (item.IsError)
                {
                    _logger.LogError("CreateInvoiceCommandHandler stopped: {Errors}", item.Errors);
                    return item.Errors;
                }

                LineItems.Add(item.Value);
            }


            var orderTypeResult = order.GetAssciatedInvoiceType();

            if (orderTypeResult.IsError) return orderTypeResult.Errors; 

            var entityResult = Domain.Invoices.Invoice.Create(
                invoiceId,
                orderTypeResult.Value,
                order.DiscountAmount ?? 0,
                LineItems,
                order.Id);

            if (entityResult.IsError)
            {
                _logger.LogError("CreateInvoiceCommandHandler stopped: {Errors}", entityResult.Errors);
                return entityResult.Errors;
            }

            var issueResult = order.IssueInvoice(entityResult.Value);

            if (issueResult.IsError)

            {

                _logger.LogError("CreateInvoiceCommandHandler stopped because an error result was returned: {ErrorResult}.", "issueResult.Errors");
                return issueResult.Errors;

            }

            _logger.LogInformation("CreateInvoiceCommandHandler is adding new entity data to the context.");
            await _context.InvoiceLineItems.AddRangeAsync(LineItems, cancellationToken);
            await _context.Invoices.AddAsync(entityResult.Value, cancellationToken);
            _logger.LogInformation("CreateInvoiceCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateInvoiceCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Invoice), cancellationToken);

            _logger.LogInformation("Invoice created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}


using Domain.Invoices;
using Contract.Features.Transactions.Invoice.DTOs;
using Contract.Features.Transactions.Orders.Mappers;

namespace Contract.Features.Transactions.Invoice.Mappers
{
    public static class InvoiceMapper
    {
        public static InvoiceLineItemDto ToDto(this Domain.Invoices.InvoiceLineItem entity) {

            return new InvoiceLineItemDto() { 
                Description = entity.Description,
                LineNo = entity.LineNo,
                Quantity = entity.Quantity,
                Tax = entity.Tax,
                UnitPrice = entity.UnitPrice,
                TotalAmount = entity.TotalAmount,
                
            };
        }
        public static InvoiceDto ToDto(this Domain.Invoices.Invoice entity)
        {
            return new InvoiceDto
            {
                Status = entity.Status.ToString(),
                InvoiceType = entity.InvoiceType.ToString(),
                DiscountAmount = entity.DiscountAmount,
                InvoiceId = entity.Id ,
               InvoiceLineItems = entity.LineItems.Select(ToDto).ToList(),
               OrderId = entity.OrderId ,
               Order = entity.Order?.ToDto(),
               NetAmount = entity.NetAmount,
               SubTotalAmount = entity.SubTotalAmount,
               TaxAmount = entity.TaxAmount
               
             };
        }
    }
}


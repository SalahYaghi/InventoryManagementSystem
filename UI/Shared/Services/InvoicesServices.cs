using Contract.Requests.Invoices;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using UI.Shared.Helpers.IO_Helper;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Invoices;

namespace UI.Services
{
    public static class InvoicesServices
    {
        public static async Task<ApiResult<InvoiceDto>> Get(Guid invoiceId)
        {
            var response = await _inventoryClient.GetAsync(GetById(invoiceId));
            return await ReadResponse<InvoiceDto>(response);
        }
        public static async Task<ApiResult<FileResponse>> GetPdfbyId(Guid invoiceId)
        {
            var response = await _inventoryClient.GetAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Invoices.GetPdfById(invoiceId));

            if (!response.IsSuccessStatusCode) {

                return ApiResult<FileResponse>.Failure("Error Generating Pdf");
            }

            var bytes =  await response.Content.ReadAsByteArrayAsync();
            var fileName = response.Content.Headers.ContentDisposition?.FileName?.Trim();

            if (string.IsNullOrEmpty(fileName))
                fileName = $"{Guid.NewGuid()}.pdf";

            return new FileResponse() { 
                FileName = fileName,
                FileBytes = bytes,
                ContentType = "application/pdf"
            };
        }

        public static async Task<ApiResult<InvoiceDto>> Create(CreateInvoiceRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(InvoicesRoute, content);
            return await ReadResponse<InvoiceDto>(response);
        }
    }
}


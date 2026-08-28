using Contract.Requests.Documents;
using Contract.Requests.People;
using Contract.Responses;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using UI.Shared.MemoryData;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.People;

namespace UI.Services
{
        public static class PeopleServices
        {
            public static async Task<ApiResult<PaginatedList<PersonForListDto>>> GetAll(bool firstCall,int pageNumber = 1
                , int pageSize = 60)
            {
            _inventoryClient.DefaultRequestHeaders.Remove("If-None-Match");

            if (!firstCall && ETagsCollection.KeyTags.TryGetValue(UI.Shared.MemoryData.Entities.People, out string tag))
                 _inventoryClient.DefaultRequestHeaders.TryAddWithoutValidation("If-None-Match", tag);
   
            var response = await _inventoryClient.GetAsync(
                    $"{PeopleRoute}?pageNumber={pageNumber}&pageSize={pageSize}");

            var etag = response.Headers.ETag?.Tag ?? string.Empty;

            if (!string.IsNullOrEmpty(etag))
            {
                ETagsCollection.KeyTags[UI.Shared.MemoryData.Entities.People ] = etag;
            }

            return await ReadResponse<PaginatedList<PersonForListDto>>(response);
            
        }

            public static async Task<ApiResult<PersonDto>> Get(Guid personId)
            {
                var response = await _inventoryClient.GetAsync(GetById(personId));

                return await ReadResponse<PersonDto>(response);
            }

            public static async Task<ApiResult<PersonDto>> Create(CreatePersonRequest request)
            {
                var json = JsonConvert.SerializeObject(request);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response = await _inventoryClient.PostAsync(PeopleRoute, content);

                return await ReadResponse<PersonDto>(response);
            }


        public static async Task<ApiResult<DocumentDto>> CreatePersonDocument( CreatePersonDocumentRequest request)
        {

            var form = new MultipartFormDataContent();

            form.Add(new StringContent(((int)request.Document.DocumentType).ToString()), "DocumentType");

            if (request.Document.DocumentImage != null && request.Document.DocumentImage.Length > 0)
            {
                var imageContent = new ByteArrayContent(request.Document.DocumentImage);
                form.Add(imageContent, "DocumentImage", "document-image");
            }

            var response = await _inventoryClient.PostAsync(Document(request.PersonId), form);
            return await ReadResponse<DocumentDto>(response);


             
        }


        public static async Task<ApiResult<PersonDto>> Update(Guid personId, UpdatePersonRequest request)
            {
                var json = JsonConvert.SerializeObject(request);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response = await _inventoryClient.PutAsync(GetById(personId), content);

                return await ReadResponse<PersonDto>(response);
            }

            public static async Task<ApiResult<bool>> Delete(Guid personId)
            {
                var response = await _inventoryClient.DeleteAsync(GetById(personId));

                if (response.IsSuccessStatusCode)
                    return ApiResult<bool>.Success(true);

                return await ReadResponse<bool>(response);
            }

            public static async Task<ApiResult<bool>> UpdatePersonImage(Guid personId, byte[] imageBytes)
            {

                var form = new MultipartFormDataContent();
            if (imageBytes != null && imageBytes.Length > 0)
            {
                var imageContent = new ByteArrayContent(imageBytes);
                form.Add(imageContent, "Image", "person-image");
            }
                var response = await _inventoryClient.PutAsync(Image(personId), (imageBytes != null && imageBytes.Length > 0) ? form : null );

                if (response.IsSuccessStatusCode)
                    return ApiResult<bool>.Success(true);

               

                return await ReadResponse<bool>(response);
            }

            public static async Task<byte[]> GetPersonImage(Guid personId)
            {
                var response = await _inventoryClient.GetAsync(Image(personId));

                if (!response.IsSuccessStatusCode)
                    return new byte[0];

                return await response.Content.ReadAsByteArrayAsync();
            }

        }
    }


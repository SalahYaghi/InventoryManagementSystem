using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using static HotelSystemUI.HttpClients.HttpClientHelper;

using System.Threading;
using System.Threading.Tasks;
using UI.Shared.CurrentUser;
using UI.Shared.Services;
using UI.Shared.Storage;
using System.Text.Json;
using UI.HttpClient;
using System.Windows.Forms;
using UI.Shared.Services.Models;

namespace UI.Shared.HttpClient
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri);

            if (request.Content != null)
            {
                var ms = new MemoryStream();
                await request.Content.CopyToAsync(ms);
                ms.Position = 0;

                var newContent = new StreamContent(ms);

                foreach (var header in request.Content.Headers)
                {
                    newContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                clone.Content = newContent;
            }

            foreach (var header in request.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            clone.Version = request.Version;

            return clone;
        }


        protected override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
        {

            try
            {

                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {

                    var refresh = SecurityStorage.ReadRefreshToken();
                    var refreshResult = await IdentityService.GenerateJwtByRefreshToken(new Contract.Requests.Identity.JwtGenerateByRefreshTokenCommand()
                    {
                        refresh = refresh,
                        loginSource = false
                    });

                    if (refreshResult.IsSuccess)
                    {
                        response.Dispose();

                        CurrentUser.CurrentUser.Jwt = refreshResult.Data.AccessToken;
                        SecurityStorage.StoreRefreshToken(refreshResult.Data.RefreshToken);

                        if (CurrentUser.CurrentUser.User != null)
                            _inventoryClient.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                                CurrentUser.CurrentUser.Jwt);


                        var originalRequest = await CloneHttpRequestMessageAsync(request);

                        originalRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                                 CurrentUser.CurrentUser.Jwt);


                        var res = await base.SendAsync(originalRequest, cancellationToken);



                        return res;
                    }
                    else
                    {

                        var content = await response.Content.ReadAsStringAsync();

                        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent(content, Encoding.UTF8, "application/problem+json")
                        };

                    }
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                     

                    return new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("Access Denied", Encoding.UTF8, "application/problem+json")
                    };
                }
                return response;

            }
            catch (Exception ex)
            {

 
                ProblemDetails problem = new ProblemDetails()
                {

                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                 };  

                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonSerializer.Serialize(problem), Encoding.UTF8, "application/problem+json")
                };

            }

        }
    }

 }
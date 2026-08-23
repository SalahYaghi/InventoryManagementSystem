using Contract.Requests.Identity;
using Infrastructure.Identity;
using Newtonsoft.Json;
using OldContract.Features.User.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Jwt;

namespace UI.Shared.Services
{
    public class IdentityService
    {
        public static async  Task<ApiResult<JwtDto>> GenerateJwt(JwtGeneratCommand jwt ) {
          
            var json = JsonConvert.SerializeObject(jwt);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(GeneareJwtRequest(), content);
            return await ReadResponse<JwtDto>(response);


        }
        public static async Task<ApiResult<JwtDto>> GenerateJwtByRefreshToken(JwtGenerateByRefreshTokenCommand jwt)
        {

            var json = JsonConvert.SerializeObject(jwt);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(Jwt.GeneareJwtByRefreshTokenRequest(), content);
            return await ReadResponse<JwtDto>(response);


        }
    }
}


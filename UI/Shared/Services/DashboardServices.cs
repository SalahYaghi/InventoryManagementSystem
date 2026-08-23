using Contract.Features.Dashboard.Dtos;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;

using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Dashboard;

namespace UI.Shared.Services
{
    public class DashboardServices
    {

        public static async Task<ApiResult<DashboardDto>> Get ()
        {
            var response = await _inventoryClient.GetAsync($"{DashboardRoute}");
            return await ReadResponse<DashboardDto>(response);
        }

    }
}

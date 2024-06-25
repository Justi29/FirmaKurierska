using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FirmaKurierska.SharedKernel.Dto;
using Newtonsoft.Json;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllForClient(int clientId);
    }
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderDto>> GetAllForClient(int clientId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5218/Order/client/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(content);
                return orders;
            }
            return new List<OrderDto>();
        }
    }
}

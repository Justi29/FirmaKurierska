using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FirmaKurierska.Domain.Models;
using FirmaKurierska.SharedKernel.Dto;
using Newtonsoft.Json;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllForClient(int clientId);
        Task<IEnumerable<OrderDto>> GetAllForCourier(int courierId);
        Task<IEnumerable<OrderDto>> GetAllWithoutCourier();
        Task<bool> UpdateOrder(UpdateOrderDto dto);
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

        public async Task<IEnumerable<OrderDto>> GetAllForCourier(int courierId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5218/Order/courier/{courierId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(content);
                return orders;
            }
            return new List<OrderDto>();
        }
        public async Task<IEnumerable<OrderDto>> GetAllWithoutCourier()
        {
            var response = await _httpClient.GetAsync($"http://localhost:5218/Order/withoutCourier");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(content);
                return orders;
            }
            return new List<OrderDto>();
        }

        public async Task<bool> UpdateOrder(UpdateOrderDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5218/Order/{dto.Id}", dto);
            return response.IsSuccessStatusCode;
        }
    }
}

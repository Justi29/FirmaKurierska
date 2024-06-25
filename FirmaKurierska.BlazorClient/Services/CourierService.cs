using FirmaKurierska.SharedKernel.Dto;
using Newtonsoft.Json;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface ICourierService
    {
        Task<CourierDto> GetById(int id);
    }
    public class CourierService : ICourierService
    {
        private readonly HttpClient _httpClient;

        public CourierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CourierDto> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5218/Courier/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var courier = JsonConvert.DeserializeObject<CourierDto>(content);
                return courier;
            }
            return new CourierDto();
        }
    }
}

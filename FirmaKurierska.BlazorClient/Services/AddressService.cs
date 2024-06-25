using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FirmaKurierska.SharedKernel.Dto;
using Newtonsoft.Json;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllForClient(int clientId);
    }
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AddressDto>> GetAllForClient(int clientId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5218/Address/client/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var addresses = JsonConvert.DeserializeObject<IEnumerable<AddressDto>>(content);
                return addresses;
            }
            return new List<AddressDto>();
        }
    }

}

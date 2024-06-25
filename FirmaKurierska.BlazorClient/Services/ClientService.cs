using FirmaKurierska.Domain.Models;
using FirmaKurierska.SharedKernel.Dto;
using Newtonsoft.Json;


namespace FirmaKurierska.BlazorClient.Services
{
    public class ClientService : IClientService
    {

        private readonly HttpClient _httpClient;
        public ClientService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/client");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var clients = JsonConvert.DeserializeObject<IEnumerable<ClientDto>>(content);
                return clients;
            }
            return new List<ClientDto>();
        }

        public async Task<ClientDto> GetById(int clientId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5218/Client/{clientId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ClientDto>(content);
                return result;
            }
            return new ClientDto();
        }
    }
}

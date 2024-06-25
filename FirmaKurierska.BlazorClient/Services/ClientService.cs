using FirmaKurierska.SharedKernel.Dto;
using static FirmaKurierska.BlazorClient.Pages.Clients.ClientList;
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
    }
}

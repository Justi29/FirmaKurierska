﻿using FirmaKurierska.Domain.Models;
using FirmaKurierska.SharedKernel.Dto;
using Newtonsoft.Json;
using System.Text;


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
        public async Task Update(ClientDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"http://localhost:5218/Client/{dto.Id}", data);
            response.EnsureSuccessStatusCode();
        }
    }
}

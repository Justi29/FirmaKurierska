using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Domain.Models;
using FirmaKurierska.SharedKernel.Dto;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface ICourierService
    {
        Task<CourierDto> GetById(int id);
        Task Update(CourierDto dto);
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

        public async Task Update(CourierDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"http://localhost:5218/Courier/{dto.Id}", data);
            response.EnsureSuccessStatusCode();
        }



    }
}

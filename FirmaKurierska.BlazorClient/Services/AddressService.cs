using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.SharedKernel.Dto;
using Newtonsoft.Json;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllForClient(int clientId);
        Task<AddressDto> GetById(int id);
        Task UpdateAddress(UpdateAddressDto addressDto);
        Task DeleteAddress(int id);
        //Task CreateAddress(CreateAddressDto addressDto);
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

        public async Task<AddressDto> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5218/Address/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var address = JsonConvert.DeserializeObject<AddressDto>(content);
                return address;
            }
            return null;
        }

        public async Task UpdateAddress(UpdateAddressDto addressDto)
        {
            if (addressDto == null || addressDto.Id <= 0)
            {
                throw new BadRequestException("Invalid address data");
            }

            var content = new StringContent(JsonConvert.SerializeObject(addressDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5218/Address/{addressDto.Id}", content);
            response.EnsureSuccessStatusCode(); // Throw an exception if not successful
        }

        public async Task DeleteAddress(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid address ID");
            }

            var response = await _httpClient.DeleteAsync($"http://localhost:5218/Address/{id}");
            response.EnsureSuccessStatusCode(); // Throw an exception if not successful
        }

        //public async Task CreateAddress(CreateAddressDto addressDto)
        //{
        //    if (addressDto == null)
        //    {
        //        throw new BadRequestException("Invalid address data");
        //    }

        //    var content = new StringContent(JsonConvert.SerializeObject(addressDto), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync($"http://localhost:5218/Address", content);
        //    response.EnsureSuccessStatusCode(); // Throw an exception if not successful
        //}
    }


}

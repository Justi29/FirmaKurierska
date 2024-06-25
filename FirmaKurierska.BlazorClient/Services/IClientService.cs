using FirmaKurierska.SharedKernel.Dto;
using System.Threading.Tasks;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAll();
        Task<ClientDto> GetById(int id);
        Task Update(ClientDto dto);
    }
}

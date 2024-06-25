using static FirmaKurierska.BlazorClient.Pages.Clients.ClientList;
using FirmaKurierska.SharedKernel.Dto;
using System.Threading.Tasks;

namespace FirmaKurierska.BlazorClient.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAll();
    }
}

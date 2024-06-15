using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Domain.Contracts
{
    public interface IAddressRepository : IRepository<Address>
    {
        int GetMaxId();
    }
}
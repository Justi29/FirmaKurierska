using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Domain.Contracts
{
    public interface ICourierRepository : IRepository<Courier>
    {
        int GetMaxId();
    }
    
}


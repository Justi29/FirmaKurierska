using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Domain.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetMaxId();
    }
}
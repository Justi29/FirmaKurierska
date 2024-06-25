using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Domain.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetMaxId();
        List<Order> GetAll();
        List<Order> GetAllWithoutCourier();
        List<Order> GetAllForClient(int clientId);
        List<Order> GetAllForCourier(int clientId);

    }
}
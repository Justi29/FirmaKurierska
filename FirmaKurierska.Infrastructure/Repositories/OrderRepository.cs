using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FirmaKurierska.Infrastructure.Repositories
{
    // Implementacja repozytoriów specyficznych
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public OrderRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }

        public int GetMaxId()
        {
            return _kioskDbContext.Orders.Max(x => x.Id);
        }
        public List<Order> GetAll()
        {
            return _kioskDbContext.Orders.Include(a => a.PickupLocation).Include(a => a.Destination).ToList();
        }
        public List<Order> GetAllForClient(int clientId)
        {
            return _kioskDbContext.Orders.Where(o => o.ClientId == clientId).Include(a => a.PickupLocation).Include(a => a.Destination).ToList();
        }
        public List<Order> GetAllForCourier(int courierId)
        {
            return _kioskDbContext.Orders.Where(o => o.CourierId == courierId).Include(a => a.PickupLocation).Include(a => a.Destination).ToList();
        }
    }
}


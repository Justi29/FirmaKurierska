using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Models;

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
    }
}


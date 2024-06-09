using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Infrastructure.Repositories
{
    public class CourierRepository : Repository<Courier>, ICourierRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public CourierRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }
    }
}

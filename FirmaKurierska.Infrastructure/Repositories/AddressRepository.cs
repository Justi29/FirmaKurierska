using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public AddressRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }
        public int GetMaxId()
        {
            return _kioskDbContext.Addresses.Max(x => x.Id);
        }
    }
}

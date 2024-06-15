using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Infrastructure.Repositories
{
    // Implementacja repozytoriów specyficznych
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly KioskDbContext _kioskDbContext;

        public ClientRepository(KioskDbContext context)
            : base(context)
        {
            _kioskDbContext = context;
        }

        public int GetMaxId()
        {
            return _kioskDbContext.Clients.Max(x => x.Id);
        }
    }
}
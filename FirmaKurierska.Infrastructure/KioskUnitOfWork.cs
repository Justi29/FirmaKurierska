using FirmaKurierska.Domain.Contracts;

namespace FirmaKurierska.Infrastructure
{
    // implementacja Unit of Work
    public class KioskUnitOfWork : IKioskUnitOfWork
    {
        private readonly KioskDbContext _context;

        public IClientRepository ClientRepository { get; }

        public ICourierRepository CourierRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public IAddressRepository AddressRepository { get; }

        public KioskUnitOfWork(KioskDbContext context, IClientRepository clientRepository, ICourierRepository courierRepository, IOrderRepository orderRepository, IAddressRepository addressRepository)
        {
            this._context = context;
            this.ClientRepository = clientRepository;
            this.CourierRepository = courierRepository;
            this.OrderRepository = orderRepository;
            this.AddressRepository = addressRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

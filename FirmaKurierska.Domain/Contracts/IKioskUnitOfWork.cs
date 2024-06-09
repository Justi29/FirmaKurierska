namespace SaleKiosk.Domain.Contracts
{
    public interface IKioskUnitOfWork : IDisposable
    {
        IClientRepository ClientRepository { get; }
        ICourierRepository CourierRepository { get; }
        IOrderRepository OrderRepository { get; }
        IAddressRepository AddressRepository { get; }

        void Commit();
    }
}
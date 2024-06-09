using FirmaKurierska.Application.Dto;

namespace FirmaKurierska.Application.Services
{
    public interface IOrderService
    {
        List<OrderDto> GetAll();
        OrderDto GetById(int id);
        //int Create(CreateOrderDto dto);
        //void Update(UpdateOrderDto dto);
        void Delete(int id);
    }
}


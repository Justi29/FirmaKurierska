using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Application.Services
{
    public class OrderService: IOrderService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        //public int Create(CreateOrderDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new BadRequestException("Order is null");
        //    }

        //    var id = _uow.OrderRepository.GetMaxId() + 1;
        //    var order = _mapper.Map<Order>(dto);
        //    order.Id = id;

        //    _uow.OrderRepository.Insert(order);
        //    _uow.Commit();

        //    return id;
        //}

        public void Delete(int id)
        {
            var order = _uow.OrderRepository.Get(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            _uow.OrderRepository.Delete(order);
            _uow.Commit();
        }

        public List<OrderDto> GetAll()
        {
            var orders = _uow.OrderRepository.GetAll();

            List<OrderDto> result = _mapper.Map<List<OrderDto>>(orders);
            return result;
        }

        public OrderDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var order = _uow.OrderRepository.Get(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            var result = _mapper.Map<OrderDto>(order);
            return result;
        }

        //public void Update(UpdateOrderDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new BadRequestException("No order data");
        //    }

        //    var order = _uow.OrderRepository.Get(dto.Id);
        //    if (order == null)
        //    {
        //        throw new NotFoundException("Order not found");
        //    }

        //    order.Id = dto.Id;
        //    order.Status = dto.Status;
        //    order.Size = dto.Size;
        //    order.TotalCost = dto.TotalCost;
        //    order.PickupLocation = dto.PickupLocation;
        //    order.Destination = dto.Destination;
        //    order.ShippingDate = dto.ShippingDate;
        //    order.PickupDate = dto.PickupDate;
        //    order.DeliveryDate = dto.DeliveryDate;
        //    order.ClientId = dto.ClientId;
        //    order.CourierId = dto.CourierId;

        //    _uow.Commit();
        //}
    }
}


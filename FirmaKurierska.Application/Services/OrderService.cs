﻿using AutoMapper;
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
        private readonly IAddressService _addressService;

        public OrderService(IKioskUnitOfWork unitOfWork, IMapper mapper, IAddressService addressService)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
            this._addressService = addressService;
        }

        public int Create(CreateOrderDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Order is null");
            }

            var clientExists = _uow.ClientRepository.Get(dto.ClientId) != null;
            if (!clientExists)
            {
                throw new BadRequestException("Client does not exist");
            }
            if (dto.Destination.Id == null)
            {
                var destination = new Address
                {
                    Id = _uow.AddressRepository.GetMaxId() + 1,
                    Street = dto.Destination.Street,
                    City = dto.Destination.City,
                    Country = dto.Destination.Country,
                    PostCode = dto.Destination.PostCode,
                    ClientId = dto.ClientId
                };
                dto.Destination = _mapper.Map<AddressDto>(destination);
            }
            if (dto.PickupLocation.Id == null)
            {
                var pickupLocation = new Address
                {
                    Id = _uow.AddressRepository.GetMaxId() + 1,
                    Street = dto.PickupLocation.Street,
                    City = dto.PickupLocation.City,
                    Country = dto.PickupLocation.Country,
                    PostCode = dto.PickupLocation.PostCode,
                    ClientId = dto.ClientId
                };
                dto.PickupLocation = _mapper.Map<AddressDto>(pickupLocation);
            }

            var id = _uow.OrderRepository.GetMaxId() + 1;
            var order = _mapper.Map<Order>(dto);
            order.Id = id;
            order.Status = OrderStatus.ReadyForPickup;
            order.ShippingDate = DateTime.Now;
            _uow.OrderRepository.Insert(order);
            _uow.Commit();

            return id;
        }

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

        public List<OrderDto> GetAllForClient(int clientId)
        {
            var orders = _uow.OrderRepository.GetAllForClient(clientId);

            List<OrderDto> result = _mapper.Map<List<OrderDto>>(orders);
            return result;
        }
        public List<OrderDto> GetAllForCourier(int courierId)
        {
            var orders = _uow.OrderRepository.GetAllForCourier(courierId);

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

        public void Update(UpdateOrderDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No order data");
            }

            var order = _uow.OrderRepository.Get(dto.Id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            order.Id = dto.Id;
            order.Status = dto.Status;
            order.Size = dto.Size;
            order.TotalCost = dto.TotalCost;
            order.PickupLocation = _mapper.Map<Address>(dto.PickupLocation);
            order.Destination = _mapper.Map<Address>(dto.Destination);
            order.ShippingDate = dto.ShippingDate;
            order.PickupDate = dto.PickupDate;
            order.DeliveryDate = dto.DeliveryDate;
            order.CourierId = dto.CourierId;

            _uow.Commit();
        }
    }
}


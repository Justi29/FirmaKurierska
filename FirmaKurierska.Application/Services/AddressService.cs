using AutoMapper;
using FirmaKurierska.SharedKernel.Dto;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Domain.Models;
using System.Diagnostics;

namespace FirmaKurierska.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AddressService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }
        public int Create(AddressDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Address is null");
            }

            var id = _uow.AddressRepository.GetMaxId() + 1;
            var address = _mapper.Map<Address>(dto);
            address.Id = id;

            _uow.AddressRepository.Insert(address);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var address = _uow.AddressRepository.Get(id);
            if (address == null)
            {
                throw new NotFoundException("Address not found");
            }

            _uow.AddressRepository.Delete(address);
            _uow.Commit();
        }

        public List<AddressDto> GetAll()
        {
            var address = _uow.AddressRepository.GetAll();

            List<AddressDto> result = _mapper.Map<List<AddressDto>>(address);
            return result;
        }


        public List<AddressDto> GetAllForClient(int clientId)
        {
            var address = _uow.AddressRepository.Find(a => a.ClientId == clientId);

            List<AddressDto> result = _mapper.Map<List<AddressDto>>(address);
            return result;
        }

        public AddressDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var address = _uow.AddressRepository.Get(id);
            if (address == null)
            {
                throw new NotFoundException("Address not found");
            }

            var result = _mapper.Map<AddressDto>(address);
            return result;
        }

        public void Update(UpdateAddressDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No order data");
            }

            var address = _uow.AddressRepository.Get(dto.Id);
            if (address == null)
            {
                throw new NotFoundException("Order not found");
            }

            address.Id = dto.Id;
            address.Country = dto.Country;
            address.City = dto.City;
            address.PostCode = dto.PostCode;
            address.Street = dto.Street;

            _uow.Commit();
        }
    }
}


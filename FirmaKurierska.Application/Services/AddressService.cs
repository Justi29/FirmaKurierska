using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Exceptions;

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
            var addresss = _uow.AddressRepository.GetAll();

            List<AddressDto> result = _mapper.Map<List<AddressDto>>(addresss);
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
    }
}


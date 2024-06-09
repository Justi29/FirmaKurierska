using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Exceptions;

namespace FirmaKurierska.Application.Services
{
    public class CourierService : ICourierService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CourierService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public void Delete(int id)
        {
            var courier = _uow.CourierRepository.Get(id);
            if (courier == null)
            {
                throw new NotFoundException("Courier not found");
            }

            _uow.CourierRepository.Delete(courier);
            _uow.Commit();
        }

        public List<CourierDto> GetAll()
        {
            var couriers = _uow.CourierRepository.GetAll();

            List<CourierDto> result = _mapper.Map<List<CourierDto>>(couriers);
            return result;
        }

        public CourierDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var courier = _uow.CourierRepository.Get(id);
            if (courier == null)
            {
                throw new NotFoundException("Courier not found");
            }

            var result = _mapper.Map<CourierDto>(courier);
            return result;
        }
    }
}


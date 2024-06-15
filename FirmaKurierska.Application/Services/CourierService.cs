using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Domain.Models;

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

        public int Create(CourierDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Courier is null");
            }

            var id = _uow.CourierRepository.GetMaxId() + 1;
            var courier = _mapper.Map<Courier>(dto);
            courier.Id = id;

            _uow.CourierRepository.Insert(courier);
            _uow.Commit();

            return id;
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

        public void Update(CourierDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No courier data");
            }

            var courier = _uow.CourierRepository.Get(dto.Id);
            if (courier == null)
            {
                throw new NotFoundException("Courier not found");
            }

            courier.Id = dto.Id;
            courier.Name = dto.Name;
            courier.Surname = dto.Surname;
            courier.PhoneNumber = dto.PhoneNumber;
            courier.Email = dto.Email;

            _uow.Commit();
        }
    }
}


using System;
using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Exceptions;

namespace FirmaKurierska.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ClientService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public void Delete(int id)
        {
            var client = _uow.ClientRepository.Get(id);
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }

            _uow.ClientRepository.Delete(client);
            _uow.Commit();
        }

        public List<ClientDto> GetAll()
        {
            var clients = _uow.ClientRepository.GetAll();

            List<ClientDto> result = _mapper.Map<List<ClientDto>>(clients);
            return result;
        }

        public ClientDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var client = _uow.ClientRepository.Get(id);
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }

            var result = _mapper.Map<ClientDto>(client);
            return result;
        }
    }

}


using System;
using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Contracts;
using FirmaKurierska.Domain.Exceptions;
using FirmaKurierska.Domain.Models;

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

        public int Create(ClientDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Client is null");
            }

            var id = _uow.ClientRepository.GetMaxId() + 1;
            var client = _mapper.Map<Client>(dto);
            client.Id = id;

            _uow.ClientRepository.Insert(client);
            _uow.Commit();

            return id;
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

        public void Update(ClientDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No order data");
            }

            var client = _uow.ClientRepository.Get(dto.Id);
            if (client == null)
            {
                throw new NotFoundException("Order not found");
            }

            client.Id = dto.Id;
            client.Name = dto.Name;
            client.Surname = dto.Surname;
            client.PhoneNumber = dto.PhoneNumber;
            client.Email = dto.Email;
            client.IsCompany = dto.IsCompany;
            client.NIP = dto.NIP;

            _uow.Commit();
        }
    }

}


using System;
using FirmaKurierska.Application.Dto;

namespace FirmaKurierska.Application.Services
{
    public interface IClientService
    {
        List<ClientDto> GetAll();
        ClientDto GetById(int id);
        void Delete(int id);
        void Update(ClientDto dto);
        int Create(ClientDto dto);
    }
}


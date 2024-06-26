﻿using FirmaKurierska.SharedKernel.Dto;

namespace FirmaKurierska.Application.Services
{
    public interface IAddressService
    {
        List<AddressDto> GetAll();
        List<AddressDto> GetAllForClient(int clientId);
        AddressDto GetById(int id);
        void Delete(int id);
        int Create(AddressDto dto);
        void Update(UpdateAddressDto dto);
    }
}


using FirmaKurierska.Application.Dto;

namespace FirmaKurierska.Application.Services
{
    public interface IAddressService
    {
        List<AddressDto> GetAll();
        AddressDto GetById(int id);
        void Delete(int id);
    }
}


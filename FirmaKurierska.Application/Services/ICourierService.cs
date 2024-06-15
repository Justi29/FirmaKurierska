using FirmaKurierska.Application.Dto;

namespace FirmaKurierska.Application.Services
{
    public interface ICourierService
    {
        List<CourierDto> GetAll();
        CourierDto GetById(int id);
        void Delete(int id);
        int Create(CourierDto dto);
        void Update(CourierDto dto);
    }
}


using FirmaKurierska.Application.Dto;

namespace FirmaKurierska.Application.Mappings
{
    public class KioskMappingProfile : Profile
    {
        public KioskMappingProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<Courier, CourierDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Order, OrderDto>();

        }
    }
}
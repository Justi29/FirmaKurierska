using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Application.Mappings
{
    public class FirmaKurierskaMappingProfile : Profile
    {
        public FirmaKurierskaMappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Courier, CourierDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.PickupLocation, opt => opt.MapFrom(src => src.PickupLocation))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination)); ;
        }
    }
}
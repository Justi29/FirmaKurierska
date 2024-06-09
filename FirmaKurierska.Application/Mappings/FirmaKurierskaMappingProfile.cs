using AutoMapper;
using FirmaKurierska.Application.Dto;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Application.Mappings
{
    public class FirmaKurierskaMappingProfile : Profile
    {
        public FirmaKurierskaMappingProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<Courier, CourierDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Order, OrderDto>();

        }
    }
}
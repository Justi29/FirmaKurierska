﻿namespace FirmaKurierska.SharedKernel.Dto
{
    public class AddressDto
    {
        public int? Id { get; set; } 
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public int? ClientId { get; set; }
    }
}


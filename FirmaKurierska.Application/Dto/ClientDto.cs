using System.Collections.Generic;
using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Application.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsCompany { get; set; }
        public string NIP { get; set; }
    }
}


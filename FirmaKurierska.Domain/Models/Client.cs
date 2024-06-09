using System.Collections.Generic;

namespace FirmaKurierska.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }
        public bool IsCompany { get; set; }
        public string NIP { get; set; }
        public List<Order> Orders { get; set; }
        public List<Address> Addresses { get; set; }
    }
}


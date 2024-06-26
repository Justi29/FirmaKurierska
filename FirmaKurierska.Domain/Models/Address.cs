﻿using System.Diagnostics;

namespace FirmaKurierska.Domain.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }

        public int? ClientId { get; set; }
        public Courier? Courier { get; set; }
        public Client? Client { get; set; }

        //public ICollection<Client> Clients { get; set; }
    }

}


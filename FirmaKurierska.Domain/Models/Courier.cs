using System.Collections.Generic;

namespace FirmaKurierska.Domain.Models
{
    public class Courier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
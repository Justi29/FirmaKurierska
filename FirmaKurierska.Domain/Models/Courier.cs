using System.Xml.Linq;

namespace FirmaKurierska.Domain.Models
{
    public class Courier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; } = "/images/image1.png";

        public ICollection<Address>? Addresses { get; set; } = new List<Address>();

        public ICollection<Order>? Orders { get; set; }
    }
}
namespace FirmaKurierska.Domain.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsCompany { get; set; }
        public string? NIP { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}


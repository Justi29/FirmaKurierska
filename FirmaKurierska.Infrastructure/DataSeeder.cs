using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.Infrastructure
{
    public class DataSeeder
    {
        private readonly KioskDbContext _dbContext;

        public DataSeeder(KioskDbContext context)
        {
            this._dbContext = context;
        }

        public void Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Clients.Any())
                {
                    var clients = new List<Client>
                    {
                        new Client()
                        {
                            Id = 1,
                            Name = "Joanna",
                            Surname = "Dymel",
                            Email = "jodymel@gmail.com",
                            PhoneNumber = "222333444",
                            IsCompany = true,
                            NIP = "9876543210",
                        },
                        new Client()
                        {
                            Id = 2,
                            Name = "Jan",
                            Surname = "Nowak",
                            Email = "jnowak@gmail.com",
                            PhoneNumber = "555666777",
                            IsCompany = false,
                            NIP = null,
                        },

                    };
                    _dbContext.Clients.AddRange(clients);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Couriers.Any())
                {
                    var couriers = new List<Courier>
                    {
                        new Courier()
                        {
                            Id = 1,
                            Name = "Michał",
                            Surname = "Mak",
                            Email = "mmaku@gmail.com",
                            PhoneNumber = "234567890",
                        },
                        new Courier()
                        {
                            Id = 2,
                            Name = "Beata",
                            Surname = "Rak",
                            Email = "biraczek@gmail.com",
                            PhoneNumber = "987654321",
                        },

                    };
                    _dbContext.Couriers.AddRange(couriers);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Orders.Any())
                {
                    var orders = new List<Order>
                    {
                        new Order()
                        {
                            Id = 1,
                            Status = OrderStatus.Delivered,
                            Size = OrderSize.Medium,
                            TotalCost = 150.00,
                            PickupLocation = new Address { Street = "ul. Krakowska 20/4", City = "Kraków", Country = "Poland", PostCode = "33-240" },
                            Destination = new Address { Street = "ul. Na błonie 33", City = "Kraków", Country = "Poland", PostCode = "30-233" },
                            ShippingDate = new DateTime(2024, 6, 15),
                            PickupDate = new DateTime(2024, 6, 16),
                            DeliveryDate = new DateTime(2024, 6, 17),
                            ClientId = 2,
                            CourierId = 1
                        },

                        new Order()
                        {
                            Id = 2,
                            Status = OrderStatus.Delivered,
                            Size = OrderSize.Large,
                            TotalCost = 25.00,
                            PickupLocation = new Address { Street = "ul. Jagiełły 20/4", City = "Warszawa", Country = "Poland", PostCode = "33-450" },
                            Destination = new Address { Street = "ul. Warszawska 55", City = "Warszawa", Country = "Poland", PostCode = "33-400" },
                            ShippingDate = new DateTime(2024, 6, 10),
                            PickupDate = new DateTime(2024, 6, 11),
                            DeliveryDate = new DateTime(2024, 6, 12),
                            ClientId = 1,
                            CourierId = 2,
                        },
                    };
                    _dbContext.Orders.AddRange(orders);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Addresses.Any())
                {
                    var addresses = new List<Address>
                    {
                        new Address()
                        {
                            Id = 1,
                            Street = "ul. Jagiełły 20/4",
                            City = "Warszawa",
                            Country = "Poland",
                            PostCode = "33-450",
                        },
                        new Address()
                        {
                            Id = 2,
                            Street = "ul. Warszawska 55",
                            City = "Warszawa",
                            Country = "Poland",
                            PostCode = "33-400",
                        },
                    };
                    _dbContext.Addresses.AddRange(addresses);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}

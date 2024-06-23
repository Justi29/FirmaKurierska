using FirmaKurierska.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaKurierska.SharedKernel.Dto
{
    public class CreateOrderDto
    {
        public OrderStatus Status { get; set; }
        public OrderSize Size { get; set; }
        public decimal TotalCost { get; set; }
        public AddressDto PickupLocation { get; set; }
        public AddressDto Destination { get; set; }
        public DateTime ShippingDate { get; set; }
        public int ClientId { get; set; }
    }
}

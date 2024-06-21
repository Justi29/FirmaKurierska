using FirmaKurierska.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaKurierska.Application.Dto
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public OrderStatus? Status { get; set; }
        public OrderSize? Size { get; set; }
        public double? TotalCost { get; set; }
        public AddressDto? PickupLocation { get; set; }
        public AddressDto? Destination { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? CourierId { get; set; }
    }
}

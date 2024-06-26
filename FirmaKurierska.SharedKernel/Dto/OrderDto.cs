﻿using FirmaKurierska.Domain.Models;

namespace FirmaKurierska.SharedKernel.Dto
{

    public class OrderDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public OrderSize Size { get; set; }
        public double TotalCost { get; set; }
        public Address PickupLocation { get; set; }
        public Address Destination { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int ClientId { get; set; }
        public int CourierId { get; set; }
    }
}


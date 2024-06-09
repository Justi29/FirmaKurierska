namespace FirmaKurierska.Domain.Models
{
    public enum OrderSize
    {
        Small,
        Medium,
        Large
    }

    public enum OrderStatus
    {
        Submitted,
        ReadyForPickup,
        OutForDelivery,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public OrderSize Size { get; set; }
        public decimal TotalCost { get; set; }
        public Address PickupLocation { get; set; }
        public Address Destination { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime? PickupDate { get; set; } 
        public DateTime? DeliveryDate { get; set; }
        public int ClientId { get; set; }
        public int CourierId { get; set; }
    }
}

//na podstawie ponizszego wstepnego opisu systemu, stworz dokladniejszy opis wygladu i dzialania takiego systemu, mozesz dodac czy poprawic informacje jesli uwazasz ze cos powinno zostac inaczej zaimplementowane. Oprocz tego w C# stworz klasy, ktore beda w katalogu Models reprezentujące budowany model danych. w bazie powinny sie znalezc co najmniej 4 tabele.

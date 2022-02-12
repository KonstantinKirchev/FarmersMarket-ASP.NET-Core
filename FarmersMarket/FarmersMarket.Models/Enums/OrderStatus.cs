namespace FarmersMarket.Models.Enums
{
    public enum OrderStatus
    {
        Open,
        Pending,
        Placed,
        Delivered,
        Cancelled,
        InTransit,
        PaymentDue,
        PickupAvailable,
        Problem,
        Processing,
        Returned
    }
}

namespace CineVibeClient.Models;

public class SnackOrder
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public int SnackId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public int BookingId { get; set; }
    public int CustomerId { get; set; }
    public decimal DiscountedPrice { get; set; }
}

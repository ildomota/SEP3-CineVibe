namespace CineVibeClient.Models
{
    public class Booking
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingTime { get; set; } = string.Empty; // "HH:mm"
        public string SeatNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}
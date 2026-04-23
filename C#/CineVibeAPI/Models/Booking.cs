using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models
{
    public class Booking
    {
        [Key]
        [Column("bookingid")]
        public int BookingId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("movieid")]
        public int MovieId { get; set; }
        [Column("bookingdate")]
        public DateTime BookingDate 
        { 
            get => _bookingDate; 
            set => _bookingDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        private DateTime _bookingDate;


        // Solo la data selezionata (senza orario)
        [Column("bookingtime")]
        public string BookingTime { get; set; } = string.Empty; // Ora del film selezionata (es. "16:00")
        [Column("seatnumber")]
        public string SeatNumber { get; set; } = string.Empty; // Posto selezionato
        [Column("status")]
        public string Status { get; set; } = "Pending";
    }
}






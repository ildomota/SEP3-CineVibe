using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models
{
    public class SnackOrder
    {
        [Key]
        [Column("orderid")]
        public int OrderId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }
        
         [Column("booking_id")]
         public int BookingId { get; set; }

        [Column("snackid")]
        public int SnackId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("totalprice")]
        public decimal TotalPrice { get; set; }
        
        [Column("discountedprice")]
        public decimal DiscountedPrice { get; set; }

        [Column("orderdate")]
        public DateTime OrderDate { get; set; }

        
    }
}
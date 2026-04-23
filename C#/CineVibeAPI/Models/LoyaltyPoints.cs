using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models
{
    public class LoyaltyPoints
    {
        [Key]
        [Column("loyaltyid")]
        public int LoyaltyId { get; set; }

        [Column("customer_id")]
        public int UserId { get; set; }

        [Column("points")]
        public int Points { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
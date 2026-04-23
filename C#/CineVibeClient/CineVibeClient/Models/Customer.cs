using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeClient.Models
{
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("username")]
        public required string Username { get; set; }

        [Column("password_hash")]
        public required string PasswordHash { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("status")]
        public required string Status { get; set; }

        [Column("loyaltypoints")]
        public int LoyaltyPoints { get; set; }
    }
}
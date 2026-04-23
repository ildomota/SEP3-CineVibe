using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models
{
    public class MovieReviews
    {
        [Key]
        [Column("reviewid")]
        public int ReviewId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("movieid")]
        public int MovieId { get; set; }

        [Column("rating")]
        public decimal Rating { get; set; } // Matches numeric(3,2) in DB

        [Column("reviewtext")]
        [Required]
        public string ReviewText { get; set; } = string.Empty;

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}
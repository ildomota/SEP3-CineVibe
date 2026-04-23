using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models
{
    public class Snacks
    {
        [Key]
        [Column("snackid")]
        public int SnackId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }

        [Column("imagepath")]
        public string? ImagePath { get; set; } // Nullable
    }

}
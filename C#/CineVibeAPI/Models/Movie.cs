using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models
{
    public class Movie
    {
        [Key]
        [Column("movieid")]
        public int movieid { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("releasedate")]
        public DateTime ReleaseDate { get; set; }

        [Column("rating")]
        public decimal Rating { get; set; }

        [Column("genre")]
        public string? Genre { get; set; }
        
        [Column("posterurl")]
        public string? PosterURL { get; set; }
        
        [Column("backgroundimageurl")]
        public string? BackgroundImageUrl { get; set; }
    }
}
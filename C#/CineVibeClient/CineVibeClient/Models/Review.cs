namespace CineVibeClient.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string PosterURL { get; set; } = string.Empty; // URL dell'immagine del film
        public int Rating { get; set; }
        public string ReviewText { get; set; } = string.Empty;
    }
}
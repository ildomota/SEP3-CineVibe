// File: Models/Movie.cs
public class Movie
{
    public int movieid { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double Rating { get; set; }
    public string? Genre { get; set; }
    public string? PosterURL { get; set; }
    public string? BackgroundImageUrl { get; set; }
}


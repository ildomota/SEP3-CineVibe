namespace CineVibeClient.Models;

public class Snacks
{
    public int SnackId { get; set; }
    public string Name { get; set; } = string.Empty; // Evita il warning
    public decimal Price { get; set; }

    public string? ImagePath { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models;

public class Admin
{
    [Key]
    [Column("admin_id")]
    public int AdminId { get; set; }

    [Required]
    [Column("username")]
    public required string Username { get; set; }

    [Required]
    [Column("password_hash")]
    public required string PasswordHash { get; set; }

    [Required]
    [Column("email")]
    public required string Email { get; set; }

    [Required]
    [Column("status")]
    public required string Status { get; set; } = "Admin";

    [NotMapped] // Non verrà salvato nel database
    public required string SecretCode { get; set; }
}
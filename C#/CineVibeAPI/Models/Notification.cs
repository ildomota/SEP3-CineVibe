using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineVibeAPI.Models
{
    public class Notification
    {
        [Key]
        [Column("notificationid")]
        public int NotificationId { get; set; }

        [Column("customer_id")]
        public int UserId { get; set; }

        [Column("message")]
        public required string Message { get; set; }

        [Column("isread")]
        public bool IsRead { get; set; }
    }
}
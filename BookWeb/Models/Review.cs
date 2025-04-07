using System;
using System.ComponentModel.DataAnnotations;

namespace BookWeb.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string GoogleBookId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

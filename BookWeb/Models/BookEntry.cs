using System.ComponentModel.DataAnnotations;

namespace BookWeb.Models
{
    public class BookEntry
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public string GoogleBookId { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public string Status { get; set; } // "Przeczytane", "Do przeczytania"
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookWeb.Models;

public class UserBook
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string GoogleBookId { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Authors { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = string.Empty;

    public string? Thumbnail { get; set; }
    public string? Description { get; set; }

    public double Rating { get; set; } = 0.0;
}

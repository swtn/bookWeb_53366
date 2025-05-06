namespace BookWeb.Models;

public class Book{
    public string Id { get; set; } = null!;
    public string Title {get; set;} = null!;
    public string Authors { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Thumbnail { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string ISBN { get; set; } = null!;
}


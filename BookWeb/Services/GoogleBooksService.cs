using System.Net.Http;
using System.Text.Json;
using BookWeb.Models;

namespace BookWeb.Services;

public class GoogleBooksService
{
    private readonly HttpClient _httpClient;

    public GoogleBooksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Book>> SearchBooksAsync(string query)
    {
        var url = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(query)}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return new List<Book>();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);

        var books = new List<Book>();
        if (doc.RootElement.TryGetProperty("items", out var items))
        {
            foreach (var item in items.EnumerateArray())
            {
                var id = item.GetProperty("id").GetString() ?? "";
                var volumeInfo = item.GetProperty("volumeInfo");

                books.Add(new Book
                {
                    Id = id,
                    Title = volumeInfo.GetProperty("title").GetString() ?? "Brak tytułu",
                    Authors = volumeInfo.TryGetProperty("authors", out var authors)
                        ? string.Join(", ", authors.EnumerateArray().Select(a => a.GetString()))
                        : "Nieznani autorzy",
                    Description = volumeInfo.TryGetProperty("description", out var desc)
                        ? desc.GetString()
                        : "",
                    Thumbnail = volumeInfo.TryGetProperty("imageLinks", out var img) && img.TryGetProperty("thumbnail", out var thumb)
                        ? thumb.GetString()
                        : ""
                });
            }
        }

        

        return books;
    }

    public async Task<Book?> GetBookByIdAsync(string id)
{
    var url = $"https://www.googleapis.com/books/v1/volumes/{Uri.EscapeDataString(id)}";
    var response = await _httpClient.GetAsync(url);

    if (!response.IsSuccessStatusCode)
        return null;

    using var stream = await response.Content.ReadAsStreamAsync();
    using var doc = await JsonDocument.ParseAsync(stream);

    var item = doc.RootElement;

    if (item.ValueKind == JsonValueKind.Undefined || item.ValueKind == JsonValueKind.Null)
        return null;

    var volumeInfo = item.GetProperty("volumeInfo");

    return new Book
    {
        Id = id,
        Title = volumeInfo.GetProperty("title").GetString() ?? "Brak tytułu",
        Authors = volumeInfo.TryGetProperty("authors", out var authors)
            ? string.Join(", ", authors.EnumerateArray().Select(a => a.GetString()))
            : "Nieznani autorzy",
        Description = volumeInfo.TryGetProperty("description", out var desc)
            ? desc.GetString()
            : "",
        Thumbnail = volumeInfo.TryGetProperty("imageLinks", out var img) && img.TryGetProperty("thumbnail", out var thumb)
            ? thumb.GetString()
            : ""
    };
}

}

using BookWeb.Models;
using System.Net.Http;
using System.Text.Json;

namespace BookWeb.Services;

public class GoogleBooksService{
    private readonly HttpClient _httpClient;
    
    public GoogleBooksService(HttpClient httpClient){
        _httpClient = httpClient;
    }

    public async Task<List<Book>> SearchBookAsync(string query){
        var url = $"";
        var response = await _httpClient.GetAsync(url);

        if(!response.IsSuccessStatusCode) return new List<Book>();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);

        var books = new List<Book>();
        var items = doc.RootElement.GetProperty("items");

        foreach(var item in items.EnumerateArray()){
            var volumeInfo = item.GetProperty("volumeInfo");
            books.Add(new Book{
                Title = volumeInfo.GetProperty("title").GetString(),
                Authors = volumeInfo.TryGetProperty("authors", out var authors) ? string.Join(", ", authors.EnumerateArray().Select(a => a.GetString())) : "Brak Danych",
                Description = volumeInfo.TryGetProperty("description", out var desc) ? desc.GetString() : "Brak danych",
                Thumbnail = volumeInfo.TryGetProperty("imageLinks", out var imageLinks) && imageLinks.TryGetProperty("thumbnail", out var thumb) ? thumb.GetString() : ""
            });
        }
        return books;
    }

}
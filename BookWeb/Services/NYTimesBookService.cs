using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BookWeb.Models;

namespace BookWeb.Services
{
    public class NYTimesBookService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NYTimesBookService> _logger;

        public NYTimesBookService(HttpClient httpClient, ILogger<NYTimesBookService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<List<Book>> GetTopBooksAsync(string listName = "combined-print-and-e-book-fiction")
        {
            var url = $"https://api.nytimes.com/svc/books/v3/lists/current/{listName}.json?api-key=GtXcZpQs03AGTWCJ58erVrBWVqQBahE3";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch books from NY Times");
                return new List<Book>();
            }

            var content = await response.Content.ReadAsStringAsync();
           var root = JsonSerializer.Deserialize<NYTBooksResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var books = new List<Book>();
            if (root?.Results?.Books != null)
            {
                foreach (var book in root.Results.Books)
                {
                    books.Add(new Book
                    {
                        Title = book.Title,
                        Authors = book.Author,
                        Description = book.Description,
                        ISBN = book.Primary_Isbn13,
                        Thumbnail = book.Book_Image
                    });
                }
            }

                        return books;
                    }
        public async Task<List<Book>> GetBooksFilteredAsync(string genre = "", string author = "")
        {
            var books = await GetTopBooksAsync("combined-print-and-e-book-fiction");

            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Authors.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return books;
        }
    }



    public class NYTBooksResponse
    {
        public NYTResults Results { get; set; }
    }

    public class NYTResults
    {
        public List<NYTBook> Books { get; set; }
    }

    public class NYTBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Book_Image { get; set; }
        public string Primary_Isbn13 { get; set; }
    }
}
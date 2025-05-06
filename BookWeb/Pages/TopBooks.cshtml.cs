using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookWeb.Services;
using BookWeb.Models;

namespace BookWeb.Pages;
    public class TopBooksModel : PageModel {
        private readonly NYTimesBookService _nyTimesBookService;

        public TopBooksModel(NYTimesBookService nyTimesBookService) {
            _nyTimesBookService = nyTimesBookService;
        }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; } = string.Empty;

        public List<Book> Results { get; set; } = new List<Book>();

        public async Task OnGetAsync() {
            if(!string.IsNullOrEmpty(Query)) {
                Results = await _nyTimesBookService.GetTopBooksAsync("combined-print-and-ebook-fiction");
            } else {
                Results = await _nyTimesBookService.GetTopBooksAsync();
            }
        }
    }

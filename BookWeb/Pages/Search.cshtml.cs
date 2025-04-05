using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookWeb.Models;
using BookWeb.Services;


namespace BookWeb.Pages;
public class SearchModel : PageModel{
    private readonly GoogleBooksService _bookService;
    public SearchModel(GoogleBooksService booksService){
        _bookService = booksService;
    }

    [BindProperty(SupportsGet = true)]
    public string Query {get; set;}
    public List<Book> Results { get; set; } = new();

    public async Task OnGetAsync(){
        if(!string.IsNullOrWhiteSpace(Query)){
            Results = await _bookService.SearchBookAsync(Query);
        }
    }
}
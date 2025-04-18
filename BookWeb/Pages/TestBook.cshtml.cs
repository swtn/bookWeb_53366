using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookWeb.Services;
using BookWeb.Models;

namespace BookWeb.Pages;
[IgnoreAntiforgeryToken]
public class TestBookModel : PageModel
{
    private readonly GoogleBooksService _bookService;

    public TestBookModel(GoogleBooksService bookService)
    {
        _bookService = bookService;
    }

[BindProperty(SupportsGet = true)]
public string Id { get; set; } = string.Empty;

public Book? Book { get; set; }

public async Task<IActionResult> OnGetAsync()
{
    Console.WriteLine($"[DEBUG] Otrzymano id = {Id}");

    if (string.IsNullOrEmpty(Id))
        return NotFound();

    var results = await _bookService.SearchBooksAsync($"id:{Id}");
    Book = results.FirstOrDefault();

    return Book == null ? NotFound() : Page();
}


}

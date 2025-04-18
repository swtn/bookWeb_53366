using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookWeb.Services;
using BookWeb.Models;

namespace BookWeb.Pages;

public class BookDetailsModel : PageModel
{
    private readonly GoogleBooksService _bookService;

    public BookDetailsModel(GoogleBooksService bookService)
    {
        _bookService = bookService;
    }

    [BindProperty(SupportsGet = true)]
    public string Id { get; set; } = string.Empty;

    public Book? Book { get; set; }

    public async Task<IActionResult> OnGetAsync()
{
    Console.WriteLine($"[DEBUG] BookDetails.OnGetAsync ID = {Id}");

    if (string.IsNullOrEmpty(Id))
        return NotFound();

    Book = await _bookService.GetBookByIdAsync(Id);

    if (Book == null)
    {
        Console.WriteLine("[DEBUG] Nie znaleziono książki po ID");
        return NotFound();
    }

    return Page();
}


}

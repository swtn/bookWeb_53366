using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using BookWeb.Data;
using BookWeb.Models;
using BookWeb.Services;

namespace BookWeb.Pages.BookActions;

[IgnoreAntiforgeryToken]
public class AddToListModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly GoogleBooksService _bookService;

    public AddToListModel(ApplicationDbContext db, UserManager<IdentityUser> userManager, GoogleBooksService booksService)
    {
        _db = db;
        _userManager = userManager;
        _bookService = booksService;
    }

    public async Task<IActionResult> OnPostAsync(string GoogleBookId, string Title, string Authors, string Status)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        Console.WriteLine("[DEBUG] Użytkownik niezalogowany – przekierowanie do logowania");
        return RedirectToPage("/Account/Login");
    }

    Console.WriteLine($"[DEBUG] Użytkownik: {user.Id}, GoogleBookId: {GoogleBookId}, Status: {Status}");

    var book = await _bookService.GetBookByIdAsync(GoogleBookId);

    if(book == null){
        return NotFound();
    }

    var alreadyExists = _db.UserBooks.Any(b =>
        b.UserId == user.Id && b.GoogleBookId == GoogleBookId && b.Status == Status);

    if (!alreadyExists)
    {
        Console.WriteLine("[DEBUG] Dodaję nową książkę do bazy");

        var userBook = new UserBook
        {
            UserId = user.Id,
            GoogleBookId = GoogleBookId,
            Title = Title,
            Authors = Authors,
            Status = Status,
            Thumbnail = book.Thumbnail, 
            Description = book.Description,
            Rating = 0 
        };

        _db.UserBooks.Add(userBook);
        await _db.SaveChangesAsync();
        Console.WriteLine("[DEBUG] Zapisano książkę w bazie");
    }
    else
    {
        Console.WriteLine("[DEBUG] Książka już istnieje w bazie – nie dodaję");
    }

    return Redirect($"/BookDetails?id={GoogleBookId}");
}

}

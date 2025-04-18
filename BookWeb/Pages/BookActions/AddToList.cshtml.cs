using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using BookWeb.Data;
using BookWeb.Models;

namespace BookWeb.Pages.BookActions;

[IgnoreAntiforgeryToken]
public class AddToListModel : PageModel{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public AddToListModel(ApplicationDbContext context, UserManager<IdentityUser> userManager){
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public string GoogleBookId { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? Title { get; set; }
    public string? Authors { get; set; }

    public async Task<IActionResult> OnPostAsync(){
        if(!User.Identity?.IsAuthenticated ?? true){
            return Unauthorized();
        }

        var userId = _userManager.GetUserId(User);

        var alreadyExists = _context.BookEntries.Any(b => b.UserId == userId && b.GoogleBookId == GoogleBookId);

        if(!alreadyExists){
            var entry = new BookEntry{
                UserId = userId!,
                GoogleBookId = GoogleBookId,
                Title = Title ?? "Brak tytułu",
                Authors = Authors ?? "Nieznani autorzy",
                Status = Status
            };

            _context.BookEntries.Add(entry);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/Book", new { id = GoogleBookId });
    }
}
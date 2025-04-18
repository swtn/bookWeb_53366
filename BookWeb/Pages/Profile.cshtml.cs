using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Data;

namespace BookWeb.Pages;

[IgnoreAntiforgeryToken]
public class ProfileModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileModel(ApplicationDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public List<UserBook> Books { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
{
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
        return RedirectToPage("/Account/Login");

    Books = _db.UserBooks
        .Where(b => b.UserId == user.Id)
        .ToList() ?? new List<UserBook>();  // Upewnij się, że Books nie jest null

    return Page();
}

    public async Task<IActionResult> OnPostDeleteBookAsync(int id){
        var user = await _userManager.GetUserAsync(User);
        if(user == null){
            return RedirectToPage("/Account/Login");
        }

        var bookToRemove = _db.UserBooks.FirstOrDefault(b => b.Id == id && b.UserId == user.Id);
        if(bookToRemove != null){
            _db.UserBooks.Remove(bookToRemove);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage();
    }

}

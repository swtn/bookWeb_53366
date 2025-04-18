using BookWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookWeb.Data;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages()
    .WithRazorPagesRoot("/Pages"); // <--- ważne, gdy masz Pages w folderze nadrzędnym!
builder.Services.AddHttpClient<BookWeb.Services.GoogleBooksService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapFallback(async context =>
{
    Console.WriteLine($"[FALLBACK] Nie znaleziono ścieżki: {context.Request.Path}");
    context.Response.StatusCode = 404;
    await context.Response.WriteAsync("Fallback 404 – brak dopasowania");
});

app.Run();

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
// Własny serwis Context do połączenia z bazą danych GraveyardDB
builder.Services.AddDbContext<GraveyardContext>(options =>
    // Korzystamy z SqlServer, pytamy o bazę GraveyardDB (to jest w appsettings.json)
    options.UseSqlServer(builder.Configuration.GetConnectionString("GraveyardDB")));

// Serwisy do logowania
builder.Services.AddAntiforgery();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);  // 60 minut trwania
    options.SlidingExpiration = true;  // Ponowienie włączone
    options.AccessDeniedPath = "/Forbidden/";
    options.LoginPath = "/Login";
});
// Ustawienia
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Home/Login";
});
builder.Services.AddAuthorization();  // Autoryzacja
builder.Services.AddMemoryCache();
builder.Services.AddSession();


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

app.UseAuthentication();  // Autentykacja
app.UseAuthorization();  // Autoryzacja

app.UseSession();  // Sesja

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
// W³asny serwis Context do po³¹czenia z baz¹ danych GraveyardDB
builder.Services.AddDbContext<GraveyardContext>(options =>
    // Korzystamy z SqlServer, pytamy o bazê GraveyardDB (to jest w appsettings.json)
    options.UseSqlServer(builder.Configuration.GetConnectionString("GraveyardDB")));

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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

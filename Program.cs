using BodaInvitacionApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Carpeta de la base de datos
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataPath); // Asegura que exista la carpeta
var dbPath = Path.Combine(dataPath, "invitation.db");


// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext SQLite
builder.Services.AddDbContext<InvitacionContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InvitacionContext>();
    db.Database.EnsureCreated();
}


var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"); 

app.Run();

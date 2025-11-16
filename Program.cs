using BodaInvitacionApp.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Intentamos leer la variable que Render envía.
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    // ======================================
    //        MODO LOCAL → SQLite
    // ======================================
    Console.WriteLine("➡ Modo LOCAL: usando SQLite");

    // Carpeta App_Data
    var dataPath = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
    Directory.CreateDirectory(dataPath);
    var dbPath = Path.Combine(dataPath, "invitation.db");

    builder.Services.AddDbContext<InvitacionContext>(options =>
        options.UseSqlite($"Data Source={dbPath}"));
}
else
{
    // ======================================
    //     MODO PRODUCCIÓN → PostgreSQL
    // ======================================
    Console.WriteLine("➡ Modo RENDER: usando PostgreSQL");

    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    var build = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port,
        Username = userInfo[0],
        Password = userInfo.Length > 1 ? userInfo[1] : "",
        Database = uri.AbsolutePath.TrimStart('/'),
        SslMode = SslMode.Prefer,
        TrustServerCertificate = true
    };

    builder.Services.AddDbContext<InvitacionContext>(options =>
        options.UseNpgsql(build.ConnectionString));
}

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Crea la base si no existe (SQLite y Postgres)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InvitacionContext>();
    db.Database.EnsureCreated();
}

// Pipeline MVC
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

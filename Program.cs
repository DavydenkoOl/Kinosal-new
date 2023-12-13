using Microsoft.EntityFrameworkCore;
using Kinosal.Models;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавл€ем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<KinoContext>(options => options.UseSqlServer(connection));

// ƒобавл€ем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Kinoes}/{action=Index}/{id?}");

app.Run();



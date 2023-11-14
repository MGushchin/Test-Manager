using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;


var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("TestManagerConnect");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.MapDefaultControllerRoute();

// устанавливаем сопоставление маршрутов с контроллерами
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(name: "Project", pattern: "{controller}/{action}/{projectName}");

app.Run();

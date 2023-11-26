using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;
using Test_Manager_Alpha;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

string? connection = builder.Configuration.GetConnectionString("TestManagerConnect");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

//DI
builder.Services.AddScoped<TestCaseHub>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(name: "Project", pattern: "{controller}/{action}/{projectName}");

app.MapHub<TestCaseHub>("/TestCaseEditHub");

app.Run();

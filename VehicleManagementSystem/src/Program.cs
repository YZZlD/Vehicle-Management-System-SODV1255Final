using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Data;
using VehicleManagementSystem.src.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<APPCONTEXTDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<USERREPO>();
builder.Services.AddScoped<RESERVEREPO>();
builder.Services.AddScoped<VEHICLEREPO>();
builder.Services.AddScoped<STAFFREPO>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapDefaultControllerRoute();

//Catch-all route for invalid routes
app.MapControllerRoute(
    name: "catchall",
    pattern: "{*url}",
    defaults: new {controller = "Dashboard", action = "Index"}
);

app.Run();

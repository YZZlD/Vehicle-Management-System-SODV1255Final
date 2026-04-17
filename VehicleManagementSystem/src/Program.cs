using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Data;
using Microsoft.AspNetCore.Identity;
using VehicleManagementSystem.src.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VEHICLEMODELDB>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<USERMODELDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<RESERVATIONDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<STAFFMODEL, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

//database configured to use integrated security


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

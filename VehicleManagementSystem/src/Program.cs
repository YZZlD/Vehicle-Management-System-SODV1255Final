using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VEHICLEMODELDB>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<USERMODELDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<RESERVATIONDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//database configured to use integrated security


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

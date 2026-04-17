using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Data;
using VehicleManagementSystem.src.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
// builder.Services.AddDbContext<VEHICLEMODELDB>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddDbContext<USERMODELDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddDbContext<RESERVATIONDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<APPCONTEXTDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddIdentity<STAFFMODEL, IdentityRole>(options =>
// {
//     options.Password.RequiredUniqueChars = 0;
//     options.Password.RequireUppercase = false;
//     options.Password.RequiredLength = 8;
//     options.Password.RequireLowercase = false;
//     options.Password.RequireNonAlphanumeric = false;
// });

//database configured to use integrated security

builder.Services.AddScoped<USERREPO>();
builder.Services.AddScoped<RESERVEREPO>();
builder.Services.AddScoped<VEHICLEREPO>();
builder.Services.AddScoped<STAFFREPO>();

var app = builder.Build();

app.UseSession();

app.MapDefaultControllerRoute();

//Catch-all route for invalid routes
app.MapControllerRoute(
    name: "catchall",
    pattern: "{*url}",
    defaults: new {controller = "Dashboard", action = "Index"}
);

app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Application.Mapping;
using TrafficPenaltyManagement.Application.Services.DashboardService;
using TrafficPenaltyManagement.Application.Services.EmployeeServices;
using TrafficPenaltyManagement.Application.Services.PenaltyServices;
using TrafficPenaltyManagement.Application.Services.PenaltyTypeServices;
using TrafficPenaltyManagement.Application.Services.VehicleSerivces;
using TrafficPenaltyManagement.Infrastructure.Identitiy;
using TrafficPenaltyManagement.Infrastructure.Persistence;
using TrafficPenaltyManagement.Infrastructure.Repositories;
using TrafficPenaltyManagement.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPenaltyApprovalHistoryRepository,PenaltyApprovalHistoryRepository>();
builder.Services.AddScoped<IPenaltyRepository, PenaltyRepository>();
builder.Services.AddScoped<IPenaltyService, PenaltyService>();
builder.Services.AddScoped<IPenaltyTypeRepository, PenaltyTypeRepository>();
builder.Services.AddScoped<IPenaltyTypeService, PenaltyTypeService>();
builder.Services.AddAutoMapper(typeof(GeneralMapping));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
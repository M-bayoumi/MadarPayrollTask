using Microsoft.EntityFrameworkCore;
using Payroll_Mohamed_Bayoumi.Context;
using Payroll_Mohamed_Bayoumi.Repositories.Abstractions;
using Payroll_Mohamed_Bayoumi.Repositories.Implementations;
using Payroll_Mohamed_Bayoumi.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


string connectionString = builder.Configuration.GetConnectionString("Database") ??
    throw new InvalidOperationException("Connection string 'Database' is missing.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Department}/{action=Index}/{id?}");

app.Run();

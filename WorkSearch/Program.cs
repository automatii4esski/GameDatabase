using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSearch.DBContext;
using WorkSearch.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Connection string is not found");
builder.Services.AddDbContextPool<MyDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<User, IdentityRole<long>>().AddEntityFrameworkStores<MyDBContext>();

builder.Logging.AddConsole();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StudyPlan}/{action=Index}/{id?}");

app.Run();

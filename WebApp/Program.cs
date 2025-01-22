using Microsoft.EntityFrameworkCore;
//using WebApp.Services;
using BusinessLogic;
using DataAccess;
using DotNetEnv;
using DataAccess.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//string connectionString = Env.GetString("DB_CONNECTION");

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<CinemaDbContext>(options =>
		options.UseMySql(connectionString
		, new MySqlServerVersion(new Version(10, 3, 39)))
	);


builder.Services.AddAutoMapper();

builder.Services.AddValidators();

builder.Services.AddDistributedMemoryCache();


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

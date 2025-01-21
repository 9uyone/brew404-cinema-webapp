using Microsoft.EntityFrameworkCore;
//using WebApp.Services;
using BusinessLogic;
using DataAccess;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

//string connectionString = builder.Configuration.GetConnectionString("LocalDb");
//string connectionString = "Env.GetString("DB_CONNECTION")";

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext(connectionString);

// repository 
//builder.Services.AddRepository();

// services


// auto mapper
builder.Services.AddAutoMapper();

// fluent validators
builder.Services.AddValidators();

// session configurations
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using BusinessLogic;
using BusinessLogic.Helpers;
using BusinessLogic.Services;
using BusinessLogic.TMDbService;
using DataAccess.Context;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Toycloud.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CinemaDbContext>(options =>
	options.UseMySql(
		builder.Configuration["ConnectionString"],
		new MySqlServerVersion(new Version(10, 3, 39))
	));

// Add services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<HallService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<ActorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddSingleton<TMDbApiService>();

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<CinemaDbContext>()
	.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = false;
	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	options.Cookie.SameSite = SameSiteMode.Strict;
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = TimeSpan.FromHours(int.Parse(builder.Configuration["Auth:ExpireInHours"]));
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddMvc(options =>
{
	options.ModelBinderProviders.InsertBodyOrDefaultBinding();
});

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddValidators();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApiDocument();

// ***
// Application configuration
// ***
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
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
	name: "admin",
	areaName: "admin",
	pattern: "admin/{controller}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

/*using (var scope = app.Services.CreateScope()) {
	var _roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
	await _roleService.CreateRoleAsync("User");
	await _roleService.CreateRoleAsync("Admin");
}*/

if (app.Environment.IsDevelopment())
{
	// Add OpenAPI 3.0 document serving middleware
	// Available at: http://localhost:<port>/swagger/v1/swagger.json
	app.UseOpenApi();

	// Add web UIs to interact with the document
	// Available at: http://localhost:<port>/swagger
	app.UseSwaggerUi(); // UseSwaggerUI Protected by if (env.IsDevelopment())
}

app.Run();
using BusinessLogic;
using BusinessLogic.Helpers;
using BusinessLogic.Services;
using DataAccess.Context;
using DataAccess.EntityModels;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Toycloud.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CinemaDbContext>(options =>
	options.UseMySql(
		builder.Configuration["connectionString"],
		new MySqlServerVersion(new Version(10, 3, 39))
	));

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<CinemaDbContext>()
	.AddDefaultTokenProviders();

// Add services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<HallService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<ActorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddValidators();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApiDocument();
builder.Services.AddSession();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => {
		options.TokenValidationParameters = new TokenValidationParameters {
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			ValidateLifetime = true,
		};

		options.Events = new JwtBearerEvents {
			OnMessageReceived = context => {
				context.Token = context.Request.Cookies["jwt"];
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddAuthorization();
builder.Services.AddMvc(options => {
	options.ModelBinderProviders.InsertBodyOrDefaultBinding();
});

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Auth/Login";
	options.AccessDeniedPath = "/Auth/AccessDenied";
	//options.SlidingExpiration = true; // Оновлювати куку при кожному запиті
});


// ***
// Application configuration
// ***
var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "admin",
	pattern: "admin/{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope()) {
	var _roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
	await _roleService.CreateRoleAsync("User");
	await _roleService.CreateRoleAsync("Admin");
}

if (app.Environment.IsDevelopment()) {
	// Add OpenAPI 3.0 document serving middleware
	// Available at: http://localhost:<port>/swagger/v1/swagger.json
	app.UseOpenApi();

	// Add web UIs to interact with the document
	// Available at: http://localhost:<port>/swagger
	app.UseSwaggerUi(); // UseSwaggerUI Protected by if (env.IsDevelopment())
}

app.Run();
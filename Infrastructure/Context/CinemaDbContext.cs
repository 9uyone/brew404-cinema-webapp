using DataAccess.Configurations;
using DataAccess.EntityModels;
using DataAccess.Models;
//using DotNetEnv;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
	public class CinemaDbContext : IdentityDbContext<User>
	{
		IConfiguration _configuration;

		public DbSet<Movie> Movies { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<Hall> Halls { get; set; }

		public CinemaDbContext(DbContextOptions options, IConfiguration configuration) : base(options) {
			_configuration = configuration;
		}
		public CinemaDbContext(): base() {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new MovieConfiguration());
			modelBuilder.ApplyConfiguration(new GenreConfiguration());
			modelBuilder.ApplyConfiguration(new ActorConfiguration());
			modelBuilder.ApplyConfiguration(new SessionConfiguration());
			modelBuilder.ApplyConfiguration(new HallConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				//Env.Load(EnvProperty.EnvFullPath);
				//string connectionString = Env.GetString(EnvProperty.DbConnection);
				string connectionString = _configuration["jwt:connectionString"];
				optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 3, 39)));
			}
		}
	}
}

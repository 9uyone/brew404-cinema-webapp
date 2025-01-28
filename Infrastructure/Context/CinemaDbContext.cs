using DataAccess.Configurations;
using DataAccess.EntityModels;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
	public class CinemaDbContext : DbContext
	{
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<Hall> Halls { get; set; }

		public CinemaDbContext(DbContextOptions options) : base(options) { }
		public CinemaDbContext(): base() {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new MovieConfiguration());
			modelBuilder.ApplyConfiguration(new GenreConfiguration());
			modelBuilder.ApplyConfiguration(new ActorConfiguration());
			modelBuilder.ApplyConfiguration(new SessionConfiguration());
			modelBuilder.ApplyConfiguration(new HallConfiguration());

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				string connectionString = "Server=107.174.71.14;Database=cinema_db;User=webapp;Password=SUNUuAra;";
				optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 3, 39)));
			}
		}
	}
}

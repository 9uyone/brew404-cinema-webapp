using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
	public class CinemaDbContext : DbContext
	{
		public DbSet<Movie> Movies { get; set; }


		public CinemaDbContext(DbContextOptions options) : base(options) { }
	}
}

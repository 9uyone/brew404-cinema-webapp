using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			builder.HasKey(m => m.Id);

			builder.Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(255);

			builder.Property(m => m.Overview)
				.HasMaxLength(2000);

			builder.Property(m => m.ReleaseDate)
				.IsRequired()
				.HasColumnType("datetime");

			builder.HasMany(m => m.Genres)
				.WithMany(g => g.Movies)
				.UsingEntity(j => j
					.ToTable("MovieGenres"));

			builder.HasMany(m => m.Credits)
				.WithMany(c => c.Movies)
				.UsingEntity(j => j
					.ToTable("MoviesCredits"));
		}
	}
}

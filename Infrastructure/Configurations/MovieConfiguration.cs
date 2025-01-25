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
				.HasMaxLength(40);

			builder.Property(m => m.Overview)
				.IsRequired()
				.HasMaxLength(200);


		}
	}
}

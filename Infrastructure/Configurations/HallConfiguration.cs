using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class HallConfiguration : IEntityTypeConfiguration<Hall>
	{
		public void Configure(EntityTypeBuilder<Hall> builder)
		{
			builder.HasKey(h => h.Id);

			builder.Property(h => h.Name)
				.IsRequired();

			builder.Property(h => h.NumbOfRows)
				.IsRequired();

			builder.Property(h => h.SeatsPerRow)
				.IsRequired();
		}
	}
}

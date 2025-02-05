using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class SeatConfiguration : IEntityTypeConfiguration<Seat>
	{
		public void Configure(EntityTypeBuilder<Seat> builder)
		{
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Row)
				.IsRequired();

			builder.Property(s => s.Number)
				.IsRequired();

			builder.HasOne(s => s.Hall)
				.WithMany(h => h.Seats)
				.HasForeignKey(s => s.HallId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

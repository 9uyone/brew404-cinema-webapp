using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
	{
		public void Configure(EntityTypeBuilder<Ticket> builder)
		{
			builder.HasKey(t => t.Id);

			builder.Property(t => t.Id)
				.ValueGeneratedOnAdd();

			builder.HasOne(t => t.Seat)
				.WithMany()
				.HasForeignKey(t => t.SeatId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(t => t.Session)
				.WithMany()
				.HasForeignKey(t => t.SessionId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(t => t.User)
				.WithMany()
				.HasForeignKey(t => t.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

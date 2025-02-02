using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class SessionConfiguration : IEntityTypeConfiguration<Session>
	{
		public void Configure(EntityTypeBuilder<Session> builder)
		{
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Id)
				.ValueGeneratedOnAdd();

			builder.Property(s => s.StartTime)
				.IsRequired()
				.HasColumnType("datetime");

			builder.Property(s => s.EndTime)
				.IsRequired()
				.HasColumnType("datetime");

			builder.HasOne(s => s.Movie)
				.WithMany(m => m.Sessions)
				.HasForeignKey(s => s.MovieId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(s => s.Hall)
				.WithMany(h => h.Sessions)
				.HasForeignKey(s => s.HallId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

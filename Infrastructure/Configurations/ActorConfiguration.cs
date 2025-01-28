using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class ActorConfiguration : IEntityTypeConfiguration<Actor>
	{
		public void Configure(EntityTypeBuilder<Actor> builder)
		{
			builder.HasKey(a => a.Id);

			builder.Property(a => a.Id)
				.ValueGeneratedOnAdd();

			builder.Property(a => a.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasIndex(a => a.Name)
				.IsUnique();

			builder.Property(a => a.Character)
				.HasMaxLength(100);
		}
	}
}

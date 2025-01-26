using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class CreditConfiguration : IEntityTypeConfiguration<Credit>
	{
		public void Configure(EntityTypeBuilder<Credit> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasMany(c => c.Roles)
				.WithMany(r => r.Credits)
				.UsingEntity(j => j.ToTable("CreditRoles"));
		}
	}
}

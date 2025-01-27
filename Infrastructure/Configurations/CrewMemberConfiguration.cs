using DataAccess.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class CrewMemberConfiguration : IEntityTypeConfiguration<CrewMember>
	{
		public void Configure(EntityTypeBuilder<CrewMember> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasMany(c => c.Roles)
				.WithMany(r => r.Crew)
				.UsingEntity(j => j.ToTable("CrewRoles"));
		}
	}
}

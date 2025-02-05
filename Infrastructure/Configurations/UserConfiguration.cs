using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(h => h.Id);

			builder.Property(h => h.UserName)
				.IsRequired();

			builder.Property(h => h.Email)
				.IsRequired();

			builder.Property(h => h.Role)
				.IsRequired();

			builder.Property(h => h.PasswordHash).IsRequired();
		}
	}
}

using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {

            builder.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
        }
    }
}

using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    public class MemberConfigurations : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FullName).IsRequired().HasMaxLength(100);
            builder.HasIndex(m => m.Email).IsUnique();
            builder.Property(m => m.Email).IsRequired().HasMaxLength(150);
            builder.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(m => m.DateOfBirth).IsRequired();
            builder.Property(m => m.UpdatedAt).IsRequired(false);
            builder.HasQueryFilter(m => !m.IsDeleted);
            builder.HasIndex(m => m.Email)
                   .IsUnique()
                   .HasDatabaseName("UQ_Member_Email");

            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(m => m.ApplicationUserId);

            builder.HasOne(m => m.Coach)
                   .WithMany(m => m.CoachedMembers)
                   .HasForeignKey(m => m.CoachId)
                   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}

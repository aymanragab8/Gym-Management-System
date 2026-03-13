using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    internal class AttendanceConfigurations : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasQueryFilter(a => !a.IsDeleted);

            builder.HasOne(a => a.Member)
                   .WithMany(m => m.Attendances)
                   .HasForeignKey(a => a.MemberId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

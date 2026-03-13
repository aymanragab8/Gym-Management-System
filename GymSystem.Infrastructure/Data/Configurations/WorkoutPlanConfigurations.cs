using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    public class WorkoutPlanConfigurations : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
           builder.HasKey(wp => wp.Id);
           builder.Property(wp => wp.Name).IsRequired().HasMaxLength(100);
           builder.Property(wp => wp.Description).IsRequired().HasMaxLength(300);
           builder.HasQueryFilter(wp => !wp.IsDeleted);

            builder.HasOne(wp => wp.Member)
               .WithMany(m => m.WorkoutPlans)
               .HasForeignKey(wp => wp.MemberId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

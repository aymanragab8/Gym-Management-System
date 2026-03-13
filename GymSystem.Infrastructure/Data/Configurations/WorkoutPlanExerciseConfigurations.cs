using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    internal class WorkoutPlanExerciseConfigurations : IEntityTypeConfiguration<WorkoutPlanExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlanExercise> builder)
        {
            builder.HasKey(wpe => new { wpe.WorkoutPlanId, wpe.ExerciseId });
            builder.Property(wpe => wpe.Sets).IsRequired();
            builder.Property(wpe => wpe.Reps).IsRequired();
            builder.Property(wpe => wpe.RestSeconds).IsRequired();


            builder.HasOne(wpe => wpe.WorkoutPlan)
                .WithMany(wp => wp.WorkoutPlanExercises)
                .HasForeignKey(wpe => wpe.WorkoutPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wpe => wpe.Exercise)
                .WithMany(e => e.WorkoutPlanExercises)
                .HasForeignKey(wpe => wpe.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    public class ExerciseConfigurations : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(wp => wp.Id);
            builder.Property(wp => wp.Name).IsRequired().HasMaxLength(100);
            builder.Property(wp => wp.Description).IsRequired().HasMaxLength(500);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}

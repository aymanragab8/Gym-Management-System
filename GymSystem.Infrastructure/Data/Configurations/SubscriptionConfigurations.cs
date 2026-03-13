using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    public class SubscriptionConfigurations : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {

            builder.HasOne(s => s.Member)
               .WithMany(m => m.Subscriptions)
               .HasForeignKey(s => s.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.Price).HasColumnType("decimal(10,2)");
            builder.Property(s => s.StartDate).IsRequired();
            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}

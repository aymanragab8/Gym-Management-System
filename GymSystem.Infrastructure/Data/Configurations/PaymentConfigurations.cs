using GymSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.Infrastructure.Data.Configurations
{
    internal class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount).HasColumnType("decimal(10,2)").IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.Property(p => p.PaymentMethod).IsRequired();

            builder.HasOne(p => p.Member)
                   .WithMany(m => m.Payments)
                   .HasForeignKey(p => p.MemberId)
                   .OnDelete(DeleteBehavior.Restrict);  
            
            builder.HasOne(p => p.Subscription)
                     .WithMany(s => s.Payments)
                     .HasForeignKey(p => p.SubscriptionId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Postgres.Configuration;

public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.SubscriptionType)
            .WithMany()
            .HasForeignKey(x => x.SubscriptionTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.StartDate)
            .IsRequired();
        builder.Property(x => x.EndDate)
            .IsRequired();
    }
}
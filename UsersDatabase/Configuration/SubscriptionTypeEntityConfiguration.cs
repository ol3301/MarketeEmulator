using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain;
using UsersDomain;

namespace UsersDatabase.Configuration;

public class SubscriptionTypeEntityConfiguration : IEntityTypeConfiguration<SubscriptionTypeEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionTypeEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasData(new SubscriptionTypeEntity { Id = SubscriptionType.Free, Name = "Free" },
            new SubscriptionTypeEntity { Id = SubscriptionType.Trial, Name = "Trial" },
            new SubscriptionTypeEntity { Id = SubscriptionType.Super, Name = "Super" });
    }
}
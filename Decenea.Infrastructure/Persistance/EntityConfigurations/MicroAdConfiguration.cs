using Decenea.Domain.Aggregates.AdvertisementAggregate;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class MicroAdConfiguration : AuditableAggregateTypeConfiguration<MicroAd>
{
        public override void Configure(EntityTypeBuilder<MicroAd> builder)
        {
                base.Configure(builder);
                builder.ToTable(name: "MicroAds");
                
                builder.Property(p => p.Id)
                        .HasMaxLength(26);
        }
}
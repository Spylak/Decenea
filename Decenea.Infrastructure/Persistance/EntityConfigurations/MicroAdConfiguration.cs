using Decenea.Domain.Aggregates.AdvertisementAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class MicroAdConfiguration : IEntityTypeConfiguration<MicroAd>
{
        public void Configure(EntityTypeBuilder<MicroAd> builder)
        {
                builder.ToTable(name: "MicroAds");
        }
}
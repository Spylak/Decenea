using Decenea.Domain.Entities.AdvertisementEntities;
using Decenea.Domain.Entities.LocationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class MicroAdConfiguration : IEntityTypeConfiguration<MicroAd>
{
        public void Configure(EntityTypeBuilder<MicroAd> builder)
        {
                builder.ToTable(name: "MicroAds");

                builder.HasOne(i => i.ApplicationUser)
                        .WithMany(i => i.MicroAds)
                        .HasForeignKey(i => i.ApplicationUserId);
                
                builder.HasOne(i => i.City)
                        .WithMany(i => i.MicroAds)
                        .HasForeignKey(i => i.CityId);
        }
}
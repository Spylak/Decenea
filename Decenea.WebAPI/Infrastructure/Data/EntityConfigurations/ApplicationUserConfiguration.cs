using Decenea.Domain.Entities.ApplicationUserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.WebAPI.Infrastructure.Data.EntityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
                builder.ToTable(name: "Users");
                
                builder.HasOne(i => i.City)
                        .WithMany(i => i.ApplicationUsers)
                        .HasForeignKey(i => i.CityId)
                        .IsRequired(false);
                
                builder.HasMany(i => i.MicroAds)
                        .WithOne(i => i.ApplicationUser)
                        .HasForeignKey(i => i.ApplicationUserId);
        }
}
using Decenea.Domain.Entities.ApplicationUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Data.EntityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
                builder.ToTable(name: "Users");
                
        }
}
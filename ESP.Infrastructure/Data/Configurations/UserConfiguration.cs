
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESP.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.IdUser);

            builder.Property(t => t.Mail)
            .IsRequired()
            .HasMaxLength(200);
            builder.Property(t => t.Password)
               .IsRequired()
               .HasMaxLength(200);

        }
    }
}

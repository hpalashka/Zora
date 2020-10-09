using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zora.Students.Domain.Models;
using Zora.Shared.Domain.Common;

namespace Zora.Students.Infrastructure.Configuration
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
               .Property(s => s.Name)
               .IsRequired()
               .HasMaxLength(ValidationConstants.MaxTitleLength);

            builder
             .Property(s => s.Email)
             .IsRequired()
             .HasMaxLength(ValidationConstants.MaxTitleLength);

            builder
                .OwnsOne(
                    s => s.PhoneNumber,
                    p =>
                    {
                        p.WithOwner();

                        p.Property(pn => pn.Number);
                    });
        }
    }
}

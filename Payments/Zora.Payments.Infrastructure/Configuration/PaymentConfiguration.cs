using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zora.Payments.Domain.Models;
using Zora.Shared.Domain.Common;

namespace Zora.Payments.Infrastructure.Configuration
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder
                .HasKey(p => p.Id);

    
            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(ValidationConstants.MaxPaymentTitleLength);

            builder
                .Property(p => p.Amount)
                .IsRequired();

            builder
                .OwnsOne(
                    p => p.PaymentDue,
                    pd =>
                    {
                        pd.WithOwner();

                        pd.Property(pe => pe.End);
                    });

     
        }
    }
}

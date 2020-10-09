using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zora.Payments.Domain.Models;
using Zora.Shared.Data;

namespace Zora.Payments.Infrastructure.Persistance
{
    public class PaymentsDbContext : MessageDbContext, IPaymentsDbContext
    {
        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; } = default!;

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();

     
    }
}

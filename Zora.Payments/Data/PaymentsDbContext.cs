using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zora.Payments.Data.Models;
using Zora.Shared.Data;

namespace Zora.Payments.Data
{
    public class PaymentsDbContext : MessageDbContext
    {
        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}

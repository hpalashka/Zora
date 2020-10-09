using Microsoft.EntityFrameworkCore;
using Zora.Payments.Domain.Models;
using Zora.Shared.Infrastructure.Persistence;

namespace Zora.Payments.Infrastructure
{   
    internal interface IPaymentsDbContext : IDbContext
    {
        DbSet<Payment> Payments { get; }

    }
}

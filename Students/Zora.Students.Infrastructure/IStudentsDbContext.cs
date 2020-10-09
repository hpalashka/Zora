using Microsoft.EntityFrameworkCore;
using Zora.Students.Domain.Models;
using Zora.Shared.Infrastructure.Persistence;

namespace Zora.Students.Infrastructure
{   
    internal interface IStudentsDbContext : IDbContext
    {
        DbSet<Student> Students { get; }

    }
}

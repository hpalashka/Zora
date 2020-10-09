using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zora.Students.Domain.Models;
using Zora.Shared.Data;


namespace Zora.Students.Infrastructure.Persistance
{
    public class StudentsDbContext : MessageDbContext, IStudentsDbContext

    {
        public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zora.Shared.Data;
using Zora.Students.Data.Models;



namespace Zora.Students.Data
{
    public class StudentsDbContext : MessageDbContext
    {
        public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
            : base(options)
        {
        }
    
        public DbSet<Student> Students { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}

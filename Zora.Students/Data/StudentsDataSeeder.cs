using System.Linq;
using Zora.Shared.Services;
using Zora.Students.Data;
using Zora.Students.Data.Models;

namespace Zora.Students.Data
{
    public class StudentsDataSeeder : IDataSeeder
    {
        private readonly StudentsDbContext db;

        public StudentsDataSeeder(StudentsDbContext db) => this.db = db;

        public void SeedData()
        {
            if (this.db.Students.Any())
            {
                return;
            }

            this.db.Students.Add(new Student
            {
                Name = "Hristina Palashka",
                Email = "hristina.palashka@gmail.com"
            });

            this.db.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zora.Shared.Services;
using Zora.Students.Data;
using Zora.Students.Data.Models;
using Zora.Students.Models.Students.ViewModels;

namespace Zora.Students.Services
{
    public class StudentsService : DataService<Student>, IStudentsService
    {

        public StudentsService(StudentsDbContext context)
       : base(context)
        { }


        public async Task<bool> DeleteStudent(int id)
        {
            var student = await Data.FindAsync<Student>(id);

            if (student == null)
            {
                return false;
            }

            Data.Remove(student);

            await Data.SaveChangesAsync();

            return true;
        }


        public async Task<Student> FindStudent(int id)
        {
            return await this.All().FirstOrDefaultAsync(c => c.Id == id);
        }


        public IEnumerable<StudentsViewModel> Students()
        {
            return this.All()
                            .Select(s => new StudentsViewModel()
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Email = s.Email
                            }
                     );
        }

    }
}

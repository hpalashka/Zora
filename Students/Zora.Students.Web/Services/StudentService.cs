using Zora.Shared.Services;
using Zora.Students.Domain.Models;
using Zora.Students.Infrastructure.Persistance;

namespace Zora.Students.Web.Services
{
    public class StudentService : DataService<Student>, IStudentService
    {
        public StudentService(StudentsDbContext context)
            : base(context)
       { }

    }
}

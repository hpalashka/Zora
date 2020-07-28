using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Shared.Services;
using Zora.Students.Data.Models;
using Zora.Students.Models.Students.ViewModels;

namespace Zora.Students.Services
{

    public interface IStudentsService : IDataService<Student>
    {
        IEnumerable<StudentsViewModel> Students();

        Task<Student> FindStudent(int id);

        Task<Student> FindStudent(string email);

        Task<bool> DeleteStudent(int id);

    }
}


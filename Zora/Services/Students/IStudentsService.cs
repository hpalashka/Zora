using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Web.Models.Students.BindingModels;
using Zora.Web.Models.Students.ViewModels;

namespace Zora.Web.Services.Students
{
    public interface IStudentsService
    {
        [Get("/Students")]
        Task<IEnumerable<StudentsViewModel>> Students();


        [Post("/Students")]
        Task<int> AddStudent(StudentBindingModel payment);


        [Delete("/Students/{id}")]
        Task<bool> DeleteStudent(int id);


    }
}

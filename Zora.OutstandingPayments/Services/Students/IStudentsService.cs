using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.OutstandingPayments.Models.Students.ViewModels;

namespace Zora.OutstandingPayments.Services.Students
{
    public interface IStudentsService
    {
        [Get("/Students")]

        Task<IEnumerable<StudentsViewModel>> Students();


    }
}

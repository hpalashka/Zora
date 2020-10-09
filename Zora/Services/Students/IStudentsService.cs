using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Students.Application.Commands.Create;
using Zora.Students.Domain.Models;
using Zora.Web.Models.Students.BindingModels;
using Zora.Web.Models.Students.ViewModels;

namespace Zora.Web.Services.Students
{
    public interface IStudentsService
    {
        [Get("/Students")]
        Task<IEnumerable<StudentsViewModel>> Students();


        [Get("/Students/{id}")]
        Task<StudentsViewModel> Student(string id);


        [Post("/Students")]
        Task<CreateStudentsOutputModel> AddStudent(StudentBindingModel student);


        [Delete("/Students/{id}")]
        Task<bool> DeleteStudent(int id);


    }
}

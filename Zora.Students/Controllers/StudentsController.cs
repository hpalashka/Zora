using MassTransit;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Shared.Controllers;
using Zora.Shared.Data.Models;
using Zora.Shared.Infrastructure;
using Zora.Shared.Messages.Students;
using Zora.Students.Data.Models;
using Zora.Students.Models.Students.BindingModels;
using Zora.Students.Models.Students.ViewModels;
using Zora.Students.Services;

namespace Zora.Students.Controllers
{
    [AuthorizeAdministrator]
    public class StudentsController : ApiController
    {
        private readonly IStudentsService _studentsService;
        private readonly IBus _publisher;


        public StudentsController(IStudentsService studentsService, IBus publisher)
        {
            _studentsService = studentsService;
            _publisher = publisher;
        }

        //todo add edit student

        [HttpPost]
        [AllowAnonymous]
        public async Task<int> AddStudent(StudentBindingModel student)
        {

            Student newStudent = new Student()
            {
                Name = student.Name,
                Email = student.Email
            };

            var messageData = new StudentMessage();

            var message = new Message(messageData);

            await _studentsService.Save(newStudent, message);

            await _publisher.Publish(messageData);

            await _studentsService.MarkMessageAsPublished(message.Id);

            return newStudent.Id;
        }


        [HttpGet]
        public IEnumerable<StudentsViewModel> Students()
        {
            return _studentsService.Students();

        }


        [HttpGet]
        [Route(Id)]
        [AllowAnonymous]
        public async Task<StudentsViewModel> Student(string id)
        {
            return await _studentsService.FindStudent(id)
                .Select(s => new StudentsViewModel()
                {
                    Id = s.Id,
                    Email = s.Email,
                    Name = s.Name
                });

        }


        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<bool> DeletePayment(int id)
        {
            //todo rename 
            //todo delete paymenst when a student is deleted
            return await _studentsService.DeleteStudent(id);
        }

    }
}

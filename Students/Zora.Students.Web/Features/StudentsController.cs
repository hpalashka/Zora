using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Shared.Domain.Models;
using Zora.Shared.Infrastructure;
using Zora.Shared.Messages.Students;
using Zora.Shared.Web.Controllers;
using Zora.Students.Application.Commands.Create;
using Zora.Students.Application.Commands.Delete;
using Zora.Students.Application.Queries.Common;
using Zora.Students.Application.Queries.StudentByEmail;
using Zora.Students.Application.Queries.Students;
using Zora.Students.Domain.Models;
using Zora.Students.Domain.Repositories;
using Zora.Students.Web.Services;

namespace Zora.Students.Web.Features
{
    [AuthorizeAdministrator]
    public class StudentsController : ApiController
    {

        private readonly IBus _publisher;
        private readonly IStudentService _studentService;
        private readonly IStudentDomainRepository _studentRepository;

        public StudentsController(IBus publisher, IStudentService studentService,
                                    IStudentDomainRepository studentRepository)
        {
            _publisher = publisher;
            _studentService = studentService;
            _studentRepository = studentRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CreateStudentsOutputModel>> AddStudent(CreateStudentCommand command)
        {
            var messageData = new StudentMessage();

            var message = new Message(messageData);

            command.MessageData = message;

            var result = await Send(command);

            await _publisher.Publish(messageData);

            await _studentService.MarkMessageAsPublished(message.Id);

            return result;

        }

        //todo add code for edit student

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Students()//todo StudentsViewModel
        {
            StudentsQuery command = new StudentsQuery();
            return await Send(command);
        }


        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<bool>> DeleteStudent([FromRoute] DeleteStudentCommand command)
        {
            //todo delete paymenst when a student is deleted
            return await Send(command);
        }

    
        [HttpGet]
        [Route(Id)]
        [AllowAnonymous]
        public async Task<ActionResult<Student>> Student(string Id)
        {
            StudentByEmailQuery command = new StudentByEmailQuery();
            command.Email = Id;
            return await Send(command);
        }

    }
}


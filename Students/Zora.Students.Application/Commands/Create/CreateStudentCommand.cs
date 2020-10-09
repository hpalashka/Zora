using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zora.Shared.Domain.Models;
using Zora.Students.Domain.Factories;
using Zora.Students.Domain.Models;
using Zora.Students.Domain.Repositories;

namespace Zora.Students.Application.Commands.Create
{
    public class CreateStudentCommand : IRequest<CreateStudentsOutputModel>
    {
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public Message MessageData { get; set; } = default!;

        public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentsOutputModel>
        {
            private readonly IStudentFactory _StudentFactory;
            private readonly IStudentDomainRepository _StudentRepository;

            public CreateStudentCommandHandler(

                IStudentFactory StudentFactory,
                IStudentDomainRepository StudentRepository)
            {

                _StudentFactory = StudentFactory;
                _StudentRepository = StudentRepository;
            }

            public async Task<CreateStudentsOutputModel> Handle(
                CreateStudentCommand request,
                CancellationToken cancellationToken)
            {
                var Student = _StudentFactory
                  .WithName(request.Name)
                  .WithEmail(request.Email)
                  .WithPhoneNumber(request.PhoneNumber)
                  .Build();

                await _StudentRepository.Save(Student, cancellationToken, request.MessageData);

                return new CreateStudentsOutputModel(Student.Id);
            }
        }
    }
}

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zora.Students.Application.Repositories;
using Zora.Students.Domain.Models;
using Zora.Students.Domain.Repositories;

namespace Zora.Students.Application.Queries.StudentByEmail
{
    public class StudentByEmailQuery : IRequest<Student>
    {
        public string Email { get; set; }
        public class StudentByEmailQueryHandler : IRequestHandler<StudentByEmailQuery, Student>
        {
            private readonly IStudentDomainRepository _studentRepository;

            public StudentByEmailQueryHandler(IStudentDomainRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Student> Handle(StudentByEmailQuery request, CancellationToken cancellationToken)
            {
                return await _studentRepository.FindStudent(request.Email, cancellationToken);
            }
        }
    }
}

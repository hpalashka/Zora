using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zora.Students.Application.Queries.Common;
using Zora.Students.Application.Repositories;
using Zora.Students.Domain.Models;

namespace Zora.Students.Application.Queries.StudentByEmail
{
    public class StudentByEmailQuery : IRequest<StudentsViewModel>
    {
        public string Email { get; set; }
        public class StudentByEmailQueryHandler : IRequestHandler<StudentByEmailQuery, StudentsViewModel>
        {
            private readonly IStudentQueryRepository _studentRepository;

            public StudentByEmailQueryHandler(IStudentQueryRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<StudentsViewModel> Handle(StudentByEmailQuery request, CancellationToken cancellationToken)
            {
                return await _studentRepository.FindStudent(request.Email, cancellationToken);
            }
        }
    }
}

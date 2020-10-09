using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zora.Students.Application.Queries.Common;
using Zora.Students.Application.Repositories;
using Zora.Students.Domain.Models;

namespace Zora.Students.Application.Queries.Students
{
    public class StudentsQuery : IRequest<IEnumerable<Student>>
    {

        public class StudentsQueryHandler : IRequestHandler<StudentsQuery, IEnumerable<Student>>
        {
            private readonly IStudentQueryRepository _StudentRepository;

            public StudentsQueryHandler(IStudentQueryRepository StudentRepository)
            {
                _StudentRepository = StudentRepository;
            }

            public async Task<IEnumerable<Student>> Handle(StudentsQuery request, CancellationToken cancellationToken)
            {
               var allStudents= await _StudentRepository.Students(cancellationToken);
               return allStudents;

            }
        }
    }
}

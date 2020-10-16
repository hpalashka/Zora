using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zora.Students.Application.Queries.Common;
using Zora.Students.Application.Repositories;

namespace Zora.Students.Application.Queries.Students
{
    public class StudentsQuery : IRequest<IList<StudentsViewModel>>
    {

        public class StudentsQueryHandler : IRequestHandler<StudentsQuery, IList<StudentsViewModel>>
        {
            private readonly IStudentQueryRepository _StudentRepository;

            public StudentsQueryHandler(IStudentQueryRepository StudentRepository)
            {
                _StudentRepository = StudentRepository;
            }

            public async Task<IList<StudentsViewModel>> Handle(StudentsQuery request, CancellationToken cancellationToken)
            {
               var allStudents= await _StudentRepository.Students(cancellationToken);
               return allStudents;

            }
        }
    }
}

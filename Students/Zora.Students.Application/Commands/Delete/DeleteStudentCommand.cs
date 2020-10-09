using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zora.Students.Domain.Repositories;
using Zora.Shared.Application;

namespace Zora.Students.Application.Commands.Delete
{
    public class DeleteStudentCommand : EntityCommand<int>, IRequest<bool>
    {
    
        public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
        {
            private readonly IStudentDomainRepository _StudentRepository;

            public DeleteStudentCommandHandler(IStudentDomainRepository StudentRepository)
            {
                _StudentRepository = StudentRepository;
            }

            public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            {
                return await _StudentRepository.DeleteStudent(request.Id, cancellationToken);
            }
        }
    }
}

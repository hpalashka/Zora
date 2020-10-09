using System.Threading;
using System.Threading.Tasks;
using Zora.Students.Domain.Models;
using Zora.Shared.Domain;

namespace Zora.Students.Domain.Repositories
{

    public interface IStudentDomainRepository : IDomainRepository<Student>
    {
        Task<Student> FindStudent(int id, CancellationToken cancellationToken = default);

        Task<Student> FindStudent(string email, CancellationToken cancellationToken = default);

        Task<bool> DeleteStudent(int id, CancellationToken cancellationToken = default);

    }
}


using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zora.Students.Application.Queries.Common;
using Zora.Students.Domain.Models;
using Zora.Shared.Application.Contracts;

namespace Zora.Students.Application.Repositories
{

    public interface IStudentQueryRepository : IQueryRepository<Student>
    {
        Task<IEnumerable<Student>> Students(CancellationToken cancellationToken = default);

    }
}


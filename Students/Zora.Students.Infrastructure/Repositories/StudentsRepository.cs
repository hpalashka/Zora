using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zora.Shared.Infrastructure.Persistence;
using Zora.Students.Application.Queries.Common;
using Zora.Students.Application.Repositories;
using Zora.Students.Domain.Models;
using Zora.Students.Domain.Repositories;

namespace Zora.Students.Infrastructure.Repositories
{
    internal class StudentsRepository : DataRepository<IStudentsDbContext, Student>,
        IStudentDomainRepository,
        IStudentQueryRepository
    {
        private readonly IMapper mapper;

        public StudentsRepository(IStudentsDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;


        public async Task<Student> FindStudent(int id, CancellationToken cancellationToken = default)
        {
            return await this.All().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Student> FindStudent(string email, CancellationToken cancellationToken = default)
        {
            return await this.All().FirstOrDefaultAsync(s => s.Email == email, cancellationToken);
        }

        public async Task<bool> DeleteStudent(int id, CancellationToken cancellationToken = default)
        {
            var Student = await Data.Students.FindAsync(id);

            if (Student == null)
            {
                return false;
            }

            Data.Students.Remove(Student);

            await Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<Student>> Students(CancellationToken cancellationToken = default)
        {
            var allStrudents = //todo this.mapper.ProjectTo<StudentsViewModel>(
              await this.All()
                .OrderBy(p => p.Name)//)
                .ToListAsync(cancellationToken);
            return allStrudents;
        }

    }
}

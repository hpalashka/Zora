using Zora.Shared.Application.Mapping;
using Zora.Students.Domain.Models;

namespace Zora.Students.Application.Queries.Common
{
    public class StudentsViewModel : IMapFrom<Student>
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }
      
    }
}

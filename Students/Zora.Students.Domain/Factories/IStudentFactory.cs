using Zora.Students.Domain.Models;
using Zora.Shared.Domain;

namespace Zora.Students.Domain.Factories
{
    public interface IStudentFactory : IFactory<Student>
    {
        IStudentFactory WithName(string name);
        IStudentFactory WithEmail(string email);
        IStudentFactory WithPhoneNumber(PhoneNumber phoneNumber);
    }
}

using Zora.Students.Domain.Models;

namespace Zora.Students.Domain.Factories
{
    internal class StudentFactory : IStudentFactory
    {
        private string studentName = default!;
        private string studentEmail = default!;
        private PhoneNumber studentPhoneNumber = default!;

        public IStudentFactory WithName(string name)
        {
            studentName = name;
            return this;
        }

        public IStudentFactory WithEmail(string email)
        {
            studentEmail = email;
            return this;
        }

        public IStudentFactory WithPhoneNumber(PhoneNumber phoneNumber)
        {
            studentPhoneNumber = phoneNumber;
            return this;
        }

        public Student Build() => new Student(studentName, studentEmail, studentPhoneNumber);

        public Student Build(string name, string email, string phoneNumber)
            => this
                .WithName(name)
                .WithEmail(email)
                .WithPhoneNumber(phoneNumber)
                .Build();

    }
}

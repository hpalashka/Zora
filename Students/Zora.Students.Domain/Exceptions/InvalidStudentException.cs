using Zora.Shared.Domain;


namespace Zora.Students.Domain.Exceptions
{
    public class InvalidStudentException : BaseDomainException
    {
        public InvalidStudentException()
        {
        }

        public InvalidStudentException(string error) => this.Error = error;
    }
}

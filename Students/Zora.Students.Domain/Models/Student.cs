using Zora.Students.Domain.Exceptions;
using Zora.Shared.Domain;
using Zora.Shared.Domain.Common;
using Zora.Shared.Domain.Models;


namespace Zora.Students.Domain.Models
{
    public class Student : Entity<int>, IAggregateRoot
    {
        internal Student(string name, string email, string phoneNumber) 
        {
            Validate(name, email);

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        private Student(string name, string email)
        {
            Name = name;
            Email = email;
            PhoneNumber = default!;
        }


        public string Name { get; set; }

        public string Email { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public Student UpdateName(string name)
        {
            ValidateName(name);
            Name = name;
            return this;
        }

        public Student UpdateEmail(string email)
        {
            ValidateEmail(email);
            Email = email;
            return this;
        }

        public Student UpdatePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        private void Validate(string name, string email)
        {
            ValidateName(name);
            ValidateEmail(email);
        }

        private void ValidateName(string name)
           => Guard.ForStringLength<InvalidStudentException>(name, ValidationConstants.MinStringLength, ValidationConstants.MaxNameLength, nameof(this.Name));

        private void ValidateEmail(string email)
         => Guard.ForStringLength<InvalidStudentException>(email, ValidationConstants.MinEmailLength, ValidationConstants.MaxEmailLength, nameof(this.Email));


    }

}

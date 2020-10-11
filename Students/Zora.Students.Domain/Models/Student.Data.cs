using System;
using System.Collections.Generic;
using Zora.Shared.Domain;

namespace Zora.Students.Domain.Models
{

    internal class StudentData : IInitialData
    {
        public Type EntityType => typeof(Student);

        public IEnumerable<object> GetData()
            => new List<Student>
            {
                new Student("Hristina Palashka", "hristina.palashka@gmail.com", "+359878138314")
            };
    }

}

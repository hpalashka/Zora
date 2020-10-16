using AutoMapper;
using Zora.Students.Application.Queries.Common;
using Zora.Students.Domain.Models;

namespace Zora.Students.Infrastructure.Persistance
{
    internal class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<Student, StudentsViewModel>()
              .ForMember(s => s.PhoneNumber, cfg => cfg
             .MapFrom(s => s.PhoneNumber.Number));
        }
    }
}
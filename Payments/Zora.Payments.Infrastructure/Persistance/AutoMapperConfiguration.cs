using AutoMapper;
using Zora.Payments.Application.Quieries.Common;
using Zora.Payments.Domain.Models;

namespace Zora.Payments.Infrastructure.Persistance
{
    internal class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<Payment, PaymentsViewModel>()
            .ForMember(d => d.PaymentDue, cfg => cfg
            .MapFrom(d => d.PaymentDue.End));

        }
    }
}
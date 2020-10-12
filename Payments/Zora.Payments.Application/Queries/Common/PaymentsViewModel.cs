using AutoMapper;
using System;
using Zora.Payments.Domain.Models;
using Zora.Shared.Application.Mapping;

namespace Zora.Payments.Application.Quieries.Common
{
    public class PaymentsViewModel : IMapFrom<Payment>

    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Amount { get; set; }

        public DateTime PaymentDue { get; private set; }

        public bool Paid { get; private set; }

        public int StudentId { get; private set; }

        public virtual void Mapping(Profile mapper)
       => mapper
           .CreateMap<Payment, PaymentsViewModel>()
           .ForMember(d => d.PaymentDue, cfg => cfg
               .MapFrom(d => d.PaymentDue.End));

    }
}

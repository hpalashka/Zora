using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Zora.Payments.Domain.Models;
using Zora.Shared.Application.Mapping;
using Zora.Shared.Domain.Common;

namespace Zora.Payments.Application.Quieries.Common
{
    public class PaymentsViewModel : IMapFrom<Payment>

    {
        public int Id { get; set; }

        [Display(Name = ValidationConstants.Title)]//todo
        public string Title { get; set; }


        [Display(Name = ValidationConstants.Amount)]
        public double Amount { get; set; }


        [Display(Name = ValidationConstants.DueDate)]
        public DateTime PaymentDue { get; private set; }


        [Display(Name = ValidationConstants.Paid)]
        public bool Paid { get; private set; }

        public int StudentId { get; private set; }

        public virtual void Mapping(Profile mapper)
       => mapper
           .CreateMap<Payment, PaymentsViewModel>()
           .ForMember(d => d.PaymentDue, cfg => cfg
               .MapFrom(d => d.PaymentDue.End));

    }
}

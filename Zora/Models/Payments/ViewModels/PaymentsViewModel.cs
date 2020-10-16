using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using Zora.Payments.Domain.Models;
using Zora.Shared.Application.Mapping;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Payments.ViewModels
{
    public class PaymentsViewModel : IMapFrom<Payment>
    {
        public int Id { get; set; }

        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Display(Name = ValidationConstants.Amount)]
        public double Amount { get; set; }


        [Display(Name = ValidationConstants.DueDate)]
        public DateTime PaymentDue { get; set; }


        [Display(Name = ValidationConstants.Paid)]
        public bool Paid { get; set; }

        public int StudentId { get; set; }

    }
}

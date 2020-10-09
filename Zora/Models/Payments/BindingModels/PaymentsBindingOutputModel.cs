using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Domain.Common;

namespace Zora.Web.Models.Payments.BindingModels
{
    public class PaymentsBindingModel
    {
        public int Id { get; set; }


        [Required]
        [StringLength(ValidationConstants.MaxPaymentTitleLength)]
        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Required]
        [Display(Name = ValidationConstants.Amount)]
        public double Amount { get; set; }

        [Required]
        [Display(Name = ValidationConstants.DueDate)]
        public DateTime DueDate { get; set; }

        public int StudentId { get; set; }
    }
}

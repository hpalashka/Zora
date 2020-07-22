using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Models.Payments.ViewModels
{
    public class PaymentsViewModel
    {
        public int Id { get; set; }

        [Display(Name = ValidationConstants.Title)]
        public string Title { get; set; }


        [Display(Name = ValidationConstants.Amount)]
        public double Amount { get; set; }


        [Display(Name = ValidationConstants.DueDate)]
        public DateTime DueDate { get; set; }


        [Display(Name = ValidationConstants.CreatedDate)]
        public DateTime CreatedDate { get; set; }


        [Display(Name = ValidationConstants.Paid)]
        public bool Paid { get; set; }

        public string UserId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Payments.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxPaymentTitleLength)]
        public string Title { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool Paid { get; set; }

        public int StudentId { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Web.Models.Statistics.ViewModels
{
      public class StatisticsViewModel 
    {
        [Display(Name = ValidationConstants.TotalStudents)]
        public int TotalStudents { get; set; }


        [Display(Name = ValidationConstants.TotalPaidAmount)]
        public double TotalPaidAmount { get; set; }


        [Display(Name = ValidationConstants.TotalAmount)]
        public double TotalAmount { get; set; }
    }
}

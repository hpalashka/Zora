using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zora.Web.Services.OutstandingPayments
{
   public interface IOutstandingPaymentsService
    {
        [Get("/OutstandingPayments")]
        Task<double> OutstandingPayments();
        
       
    }
}

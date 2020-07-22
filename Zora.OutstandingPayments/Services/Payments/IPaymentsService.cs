using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.OutstandingPayments.Models.Payments.ViewModels;
using Zora.OutstandingPayments.Models.Students.ViewModels;

namespace Zora.OutstandingPayments.Services.Payments
{
    public interface IPaymentsService
    {

        [Get("/Payments")]
        Task<IEnumerable<PaymentsViewModel>> Payments();
          
    }
}

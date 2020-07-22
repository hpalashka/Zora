using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Payments.Data.Models;
 using Zora.Payments.Models.Payments.ViewModels;
using Zora.Shared.Services;

namespace Zora.Payments.Services
{

    public interface IPaymentService : IDataService<Payment>
    {

        IEnumerable<PaymentsViewModel> Payments(int id);

        Task<Payment> FindPayment(int id);

        Task<bool> DeletePayment(int id);

        IEnumerable<PaymentsViewModel> Payments();

      
    }
}


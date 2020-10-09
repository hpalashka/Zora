using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Payments.Application.Commands.Create;
using Zora.Web.Models.Payments.BindingModels;
using Zora.Web.Models.Payments.ViewModels;

namespace Zora.Web.Services.Payments
{
    public interface IPaymentsService
    {

        [Get("/Payments/{id}")]
        Task<IEnumerable<PaymentsViewModel>> Payments(int id);


        [Post("/Payments")]
        Task<CreatePaymentOutputModel> AddPayment(PaymentsBindingOutputModel payment);


        [Delete("/Payments/{id}")]
        Task<bool> DeletePayment(int id);


        [Put("/Payments/{id}")]
        Task<bool> Pay(int id);
    }
}

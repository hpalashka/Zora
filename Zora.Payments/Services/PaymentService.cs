using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zora.Payments.Data;
using Zora.Payments.Data.Models;
using Zora.Payments.Models.Payments.ViewModels;
using Zora.Shared.Services;

namespace Zora.Payments.Services
{
    public class PaymentService : DataService<Payment>, IPaymentService

    {
        public PaymentService(PaymentsDbContext context)
            : base(context)
        { }


        public async Task<bool> DeletePayment(int id)
        {
            var payment = await Data.FindAsync<Payment>(id);

            if (payment == null)
            {
                return false;
            }

            Data.Remove(payment);

            await Data.SaveChangesAsync();

            return true;
        }


        public async Task<Payment> FindPayment(int id)
        {
            return await this.All().FirstOrDefaultAsync(c => c.Id == id);

        }


        public IEnumerable<PaymentsViewModel> Payments(int id)
        {

            return this
                 .All()
                 .Where(p => p.StudentId == id)
                 .OrderByDescending(p => p.CreatedDate)
                                  .Select(p => new PaymentsViewModel()
                                  {
                                      Id = p.Id,
                                      StudentId = p.StudentId,
                                      Amount = p.Amount,
                                      DueDate = p.DueDate,
                                      Title = p.Title,
                                      Paid = p.Paid,
                                      CreatedDate = p.CreatedDate
                                  });


        }


        public IEnumerable<PaymentsViewModel> Payments()
        {
            return this.All()
                       .OrderByDescending(p => p.CreatedDate)
                              .Select(p => new PaymentsViewModel()
                              {
                                  Id = p.Id,
                                  StudentId = p.StudentId,
                                  Amount = p.Amount,
                                  DueDate = p.DueDate,
                                  Title = p.Title,
                                  Paid = p.Paid,
                                  CreatedDate = p.CreatedDate
                              });
        }
    }
}

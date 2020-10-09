using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Zora.OutstandingPayments.Services.Payments;
using Zora.OutstandingPayments.Services.Students;
using Zora.Shared.Web.Controllers;

namespace Zora.OutstandingPayments.Controllers
{

    public class OutstandingPaymentsController : ApiController
    {
        private readonly IPaymentsService _payments;
        private readonly IStudentsService _students;


        public OutstandingPaymentsController(
            IPaymentsService payments,
            IStudentsService students)
        {
            _payments = payments;
            _students = students;
        }

        [HttpGet]
        public async Task<double> OutstandingPayments()
        {
            //todo ids?

            var students = await _students.Students();

            var studentIds = students.Select(s => s.Id);

            var outstandingPayments = await _payments.Payments();

            var outstandingPaymentsId = outstandingPayments.Select(p => p.StudentId);

            var resultId = studentIds.Intersect(outstandingPaymentsId);
          
            var Total = outstandingPayments.Where(w => resultId.Any(p => p == w.StudentId)).Where(p => p.Paid == false).Sum(s => s.Amount);

            return Total;
        }
    }
}

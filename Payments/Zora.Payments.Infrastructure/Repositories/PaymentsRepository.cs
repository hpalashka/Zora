using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zora.Payments.Application.Quieries.Common;
using Zora.Payments.Application.Repositories;
using Zora.Payments.Domain.Models;
using Zora.Payments.Domain.Repositories;
using Zora.Shared.Infrastructure.Persistence;

namespace Zora.Payments.Infrastructure.Repositories
{
    internal class PaymentsRepository : DataRepository<IPaymentsDbContext, Payment>,
        IPaymentDomainRepository,
        IPaymentQueryRepository
    {
        private readonly IMapper mapper;

        public PaymentsRepository(IPaymentsDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;


        public async Task<Payment> FindPayment(int id, CancellationToken cancellationToken = default)
        {
            return await this.All().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<bool> DeletePayment(int id, CancellationToken cancellationToken = default)
        {
            var payment = await Data.Payments.FindAsync(id);

            if (payment == null)
            {
                return false;
            }

            Data.Payments.Remove(payment);

            await Data.SaveChangesAsync(cancellationToken);

            return true;
        }


        public async Task<IList<PaymentsViewModel>> Payments(int id, CancellationToken cancellationToken = default)
        {
            return await this.mapper
                    .ProjectTo<PaymentsViewModel>(this.All().Where(p => p.StudentId == id))
                    .ToListAsync(cancellationToken);
        }

        public async Task<IList<PaymentsViewModel>> Payments(CancellationToken cancellationToken = default)
        {
            return await this.mapper
                    .ProjectTo<PaymentsViewModel>(this.All())
                    .ToListAsync(cancellationToken);
        }


    }
}

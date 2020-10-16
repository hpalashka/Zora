using AutoMapper;
using System;
using Zora.Payments.Domain.Models;
using Zora.Shared.Application.Mapping;

namespace Zora.Payments.Application.Quieries.Common
{
    public class PaymentsViewModel : IMapFrom<Payment>

    {
        public int Id { get;  set; }

        public string Title { get;  set; } = default!;

        public decimal Amount { get;  set; }

        public DateTime PaymentDue { get;  set; }

        public bool Paid { get;  set; }

        public int StudentId { get;  set; }

    }
}

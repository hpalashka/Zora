using Bogus;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using Zora.Shared.Domain.Models;

namespace Zora.Payments.Domain.Models
{
    public class PaymentFakes
    {

        public class PaymentFactory : IDummyFactory
        {
            public bool CanCreate(Type type) => type == typeof(Payment);

            public object? Create(Type type) => Data.GetPayment();

            public Priority Priority => Priority.Default;
        }

        public static class Data
        {

            public const int Id = 1;
            public const int InvalidId = 123456;

            public static IEnumerable<Payment> GetPayments(int count = 10)
                => Enumerable
                    .Range(1, count)
                    .Select(GetPayment)
                    .ToList();

            public static Payment GetPayment(int id = 1)
            {
                var Payment = new Faker<Payment>()
                    .CustomInstantiator(f => new Payment(
                        $"Some Random Title for Payment {id}",
                        f.Random.Decimal(),
                        1,
                        A.Dummy<DateTimeRange>()))
                    .Generate()
                    .SetId(id);

                return Payment;
            }
        }
    }
}

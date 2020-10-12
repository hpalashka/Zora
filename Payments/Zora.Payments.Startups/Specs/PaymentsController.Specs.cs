using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using Zora.Payments.Application.Commands.Delete;
using Zora.Payments.Application.Queries.PaymentsForUser;
using Zora.Payments.Domain.Models;
using Zora.Payments.Infrastructure.Persistance;
using Zora.Payments.Web.Features;

namespace Zora.Payments.Startups
{
    public class PaymentsControllerSpecs
    {
        [Theory]
        [InlineData(PaymentFakes.Data.Id)]
        public void DeleteShouldHaveCorrectAttributes(int id)
            => MyController<PaymentsController>
                .Calling(c => c.DeletePayment(id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                .RestrictingForHttpMethod(HttpMethod.Delete));


        [Theory]
        [InlineData(PaymentFakes.Data.Id)]
        public void DeletePaymentWithCorrectIdShouldReturnTrue(int id)
            => MyController<PaymentsController>
                .Instance(controller => controller
                .WithData(entities => entities
                .WithEntities<PaymentsDbContext>(PaymentFakes.Data.GetPayment())))
                .Calling(c => c.DeletePayment(id))
                .ShouldReturn().ActionResult<bool>(result => result.Passing(model =>
                    model.Should().BeTrue()));

        [Theory]
        [InlineData(PaymentFakes.Data.InvalidId)]
        public void DeletePaymentWithInCorrectIdShouldReturnTrue(int id)
            => MyController<PaymentsController>
                .Instance(controller => controller
                .WithData(entities => entities
                .WithEntities<PaymentsDbContext>(PaymentFakes.Data.GetPayment())))
                .Calling(c => c.DeletePayment(id))
                .ShouldReturn().ActionResult<bool>(result => result.Passing(model =>
                    model.Should().BeFalse()));


        [Theory]
        [InlineData(PaymentFakes.Data.Id)]
        public void PayPaymentShouldReturnTrue(int id)
           => MyController<PaymentsController>
               .Instance(controller => controller
               .WithData(entities => entities
               .WithEntities<PaymentsDbContext>(PaymentFakes.Data.GetPayment())))
               .Calling(c => c.Pay(id))
               .ShouldReturn().ActionResult<bool>(result => result.Passing(model =>
                   model.Should().BeTrue()));

    }
}

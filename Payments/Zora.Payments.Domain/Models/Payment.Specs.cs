//using FakeItEasy;
using FluentAssertions;
using System;
using Xunit;
using Zora.Payments.Domain.Exceptions;

namespace Zora.Payments.Domain.Models
{
   public class DealerSpecs
    {
        //[Fact]
        //public void PaymentPaidShouldMutateIsAvailable()
        //{
        //    // Arrange
        //    var payment = A.Dummy<Payment>();

        //    // Act
        //    payment.PayPayment();

        //    // Assert
        //    payment.Paid.Should().BeTrue();
        //}

        [Fact]
        public void ValidPaymentShouldNotThrowException()
        {
            // Act
            Action act = () => new Payment("Payment for august",2.63m, 1, new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(10)));

            // Assert
            act.Should().NotThrow<InvalidPaymentException>();
        }

        [Fact]
        public void InvalidTitleShouldThrowException()
        {
            // Act
            Action act = () => new Payment("", 2.63m, 1, new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(10)));


            // Assert
            act.Should().Throw<InvalidPaymentException>();
        }


    }
}

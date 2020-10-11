using FluentAssertions;
using System;
using Xunit;
using Zora.Payments.Domain.Exceptions;
using Zora.Payments.Domain.Models;

namespace Zora.Payments.Zora.Payments.Domain.Factories.Factories
{
    public class PaymentFactorySpecs
    {
        [Fact]
        public void BuildShouldThrowExceptionIfTitleIsNotSet()
        {
            // Arrange
            var paymentFactory = new PaymentFactory();

            // Act
            Action act = () => paymentFactory
                    .WithTitle("")
                    .WithAmount(4.56m)
                    .WithPaymentDue(new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(5)))
                    .WithStudent(1)
                    .Build();

            // Assert
            act.Should().Throw<InvalidPaymentException>();
        }

        [Fact]
        public void BuildShouldThrowExceptionIfAmountIsNegative()
        {

            // Arrange
            var paymentFactory = new PaymentFactory();

            // Act
            Action act = () => paymentFactory
                    .WithTitle("Payment for september")
                    .WithAmount(-3.69m)
                    .WithPaymentDue(new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(5)))
                    .WithStudent(1)
                    .Build();

            // Assert
            act.Should().Throw<InvalidPaymentException>();
        }

        [Fact]
        public void BuildShouldThrowExceptionIfDateTimeRangeNotValid()
        {
            // Arrange
            var paymentFactory = new PaymentFactory();

            // Act
            Action act = () => paymentFactory
                    .WithTitle("Payment for september")
                    .WithPaymentDue(new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(-5)))
                    .WithAmount(4.56m)
                    .WithStudent(1)
                    .Build();

            // Assert
            act.Should().Throw<InvalidDateRangeException>();
        }

        [Fact]
        public void BuildShouldCreatePaymentIfEveryPropertyIsSet()
        {
            // Arrange
            var paymentFactory = new PaymentFactory();

            // Act
            Action act = () => paymentFactory
                    .WithTitle("Payment for september")
                    .WithAmount(4.56m)
                    .WithPaymentDue(new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(5)))
                    .WithStudent(1)
                    .Build();

            // Assert
            paymentFactory.Should().NotBeNull();
        }
    }
}

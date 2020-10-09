using Microsoft.Extensions.DependencyInjection;
using Zora.Payments.Domain.Factories;
using Zora.Payments.Zora.Payments.Domain.Factories.Factories;
using Zora.Shared.Domain.Configuration;

namespace Zora.Payments.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services
                .AddCommonDomain()
                .AddTransient<IPaymentFactory, PaymentFactory>();

    }
}

using Microsoft.Extensions.DependencyInjection;
using Zora.Shared.Domain.Configuration;
using Zora.Students.Domain.Factories;

namespace Zora.Students.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services
                .AddCommonDomain()
                .AddTransient<IStudentFactory, StudentFactory>();
    }
}

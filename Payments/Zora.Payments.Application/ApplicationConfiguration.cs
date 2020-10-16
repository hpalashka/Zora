using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zora.Shared.Application.Configuration;
using MediatR;
using System.Reflection;

namespace Zora.Payments.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddCommonApplication(configuration)
            .AddMediatR(Assembly.GetExecutingAssembly());
    }
}

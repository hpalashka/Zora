using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTested.AspNetCore.Mvc;
using Zora.Identity.Data.Models;
using Zora.Identity.Services.Identity;
using Zora.Payments.Application.Repositories;
using Zora.Payments.Domain.Factories;

namespace Zora.Payments.Startups
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            ValidateServices(services);

            services.ReplaceTransient<UserManager<User>>(_ => IdentityFakes.FakeUserManager);
            services.ReplaceTransient<ITokenGeneratorService>(_ => JwtTokenGeneratorFakes.FakeJwtTokenGenerator);
        }

        private static void ValidateServices(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IPaymentFactory>();
            provider.GetRequiredService<IMediator>();
            provider.GetRequiredService<IPaymentQueryRepository>();
            provider.GetRequiredService<IControllerFactory>();
        }
    }
}

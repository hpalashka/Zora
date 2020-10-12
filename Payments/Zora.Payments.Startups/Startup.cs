using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zora.Payments.Application;
using Zora.Payments.Domain;
using Zora.Payments.Infrastructure.Persistance;
using Zora.Shared.Infrastructure;
using Zora.Payments.Web;
using Zora.Payments.Web.Services;

namespace Zora.Payments.Startups
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<PaymentsDbContext>(this.Configuration)
                .AddDomain()
                .AddApplication(this.Configuration)
                .AddInfrastructure(this.Configuration)
                .AddWebComponents()
                .AddScoped<IPaymentService, PaymentService>()
                .AddMessaging(this.Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zora.Shared.Infrastructure;
using Zora.Shared.Services;
using Zora.Statistics.Data;
using Zora.Statistics.Messages;
using Zora.Statistics.Services.Statistics;

namespace Zora.Statistics
{


    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<StatisticsDbContext>(this.Configuration)
                .AddTransient<IDataSeeder, StatisticsDataSeeder>()
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddMessaging(this.Configuration,
                    typeof(PaymentConsumer),
                    typeof(PaymentPaidConsumer),
                    typeof(StudentConsumer));
  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}

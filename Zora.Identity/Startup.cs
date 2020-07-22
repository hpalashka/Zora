using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zora.Identity.Data;
using Zora.Identity.Infrastructure;
using Zora.Identity.Services.Identity;
using Zora.Shared.Infrastructure;
using Zora.Shared.Services;

namespace Zora.Identity
{


    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<Data.IdentityDbContext>(this.Configuration)
                .AddUserStorage()
                .AddTransient<IDataSeeder, IdentityDataSeeder>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ITokenGeneratorService, TokenGeneratorService>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}

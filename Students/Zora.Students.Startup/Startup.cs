using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zora.Shared.Infrastructure;
using Zora.Shared.Students.Infrastructure;
using Zora.Students.Application;
using Zora.Students.Domain;
using Zora.Students.Web;
using Zora.Students.Infrastructure.Persistance;
using Zora.Students.Web.Services;

namespace Zora.Students
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
             => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<StudentsDbContext>(this.Configuration)
                .AddDomain()
                .AddApplication(this.Configuration)
                .AddInfrastructure(this.Configuration)
                .AddWebComponents()
                .AddScoped<IStudentService, StudentService>()
                .AddMessaging(this.Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}

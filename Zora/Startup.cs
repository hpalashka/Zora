using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using Zora.EmailService;
using Zora.Shared.Infrastructure;
using Zora.Shared.Services;
using Zora.Shared.Services.Identity;
using Zora.Web.Data;
using Zora.Web.Infrastructure;
using Zora.Web.Services;
using Zora.Web.Services.Identity;
using Zora.Web.Services.OutstandingPayments;
using Zora.Web.Services.Payments;
using Zora.Web.Services.Statistics;
using Zora.Web.Services.Students;

namespace Zora
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
               .GetSection(nameof(ServiceEndpoints))
               .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddWebService<ZoraDbContext>(this.Configuration)
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtCookieAuthenticationMiddleware>()
                .AddControllersWithViews(options => options
                    .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services
                .AddRefitClient<IIdentityService>()
                .WithConfiguration(serviceEndpoints.Identity);

            services
                .AddRefitClient<IStudentsService>()
                .WithConfiguration(serviceEndpoints.Students);

            services
                .AddRefitClient<IPaymentsService>()
                .WithConfiguration(serviceEndpoints.Payments);

            services
                 .AddRefitClient<IOutstandingPaymentsService>()
                 .WithConfiguration(serviceEndpoints.OutstandingPayments);

            services
                .AddRefitClient<IStatisticsService>()
                .WithConfiguration(serviceEndpoints.Statistics);



            var emailConfig = Configuration.GetSection("EmailConfiguration")//todo
            .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ZoraDbContext>().Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app
                //.UseHttpsRedirection()
                .UseStaticFiles()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseRouting()
                .UseJwtCookieAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                .MapDefaultControllerRoute());

        }
    }
}

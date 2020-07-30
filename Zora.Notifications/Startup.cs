using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zora.Notifications.Hubs;
using Zora.Notifications.Infrastructure;
using Zora.Notifications.Messages;
using Zora.Shared.Infrastructure;
using Zora.Shared.Messages.Payments;

namespace Zora.Notifications
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("https://localhost:5001");
            }));
            services.AddControllers();
            services.AddTokenAuthentication(this.Configuration,JwtConfiguration.BearerEvents)
                    .AddMessaging(this.Configuration,typeof(NewStundentConsumer))
                    .AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseCors("CorsPolicy")
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<NotificationsHub>("/notifications");
                });
        }
    }
}

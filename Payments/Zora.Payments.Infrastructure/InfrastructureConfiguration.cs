using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zora.Payments.Infrastructure;
using Zora.Payments.Infrastructure.Persistance;
using Zora.Shared.Application.Contracts;
using Zora.Shared.Domain;
using Zora.Shared.Infrastructure.Events;

namespace Zora.Shared.Infrastructure
{

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
           => services
               .AddDatabase(configuration)
               .AddRepositories()
              .AddTransient<IEventDispatcher, EventDispatcher>();


        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<PaymentsDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(PaymentsDbContext).Assembly.FullName)))
                .AddScoped<IPaymentsDbContext>(provider => provider.GetService<PaymentsDbContext>())
                .AddTransient<IInitializer, DatabaseInitializer>();

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IDomainRepository<>))
                        .AssignableTo(typeof(IQueryRepository<>)))
                    .AsImplementedInterfaces()
                   .WithTransientLifetime());

    }

}

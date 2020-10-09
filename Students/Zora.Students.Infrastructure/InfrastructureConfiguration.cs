using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zora.Shared.Application.Contracts;
using Zora.Shared.Domain;
using Zora.Shared.Infrastructure;
using Zora.Shared.Infrastructure.Events;
using Zora.Students.Infrastructure;
using Zora.Students.Infrastructure.Persistance;

namespace Zora.Shared.Students.Infrastructure
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
                .AddDbContext<StudentsDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sqlServer => sqlServer
                            .MigrationsAssembly(typeof(StudentsDbContext).Assembly.FullName)))
                .AddScoped<IStudentsDbContext>(provider => provider.GetService<StudentsDbContext>())
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

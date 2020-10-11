using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Xunit;
using Zora.Payments.Application.Repositories;
using Zora.Payments.Infrastructure;
using Zora.Payments.Infrastructure.Persistance;
using Zora.Shared.Infrastructure.Events;

namespace Zora.Shared.Infrastructure
{
    public class InfrastructureConfigurationSpecs
    {
        [Fact]
        public void AddRepositoriesShouldRegisterRepositories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddDbContext<PaymentsDbContext>(opts => opts
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddScoped<IPaymentsDbContext>(provider => provider
                    .GetService<PaymentsDbContext>())
                .AddTransient<IEventDispatcher, EventDispatcher>();

            // Act
            var services = serviceCollection
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddRepositories()
                .BuildServiceProvider();

            // Assert
            services
                .GetService<IPaymentQueryRepository>()
                .Should()
                .NotBeNull();
        }
    }
}

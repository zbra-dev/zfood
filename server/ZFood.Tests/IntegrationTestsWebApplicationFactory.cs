using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZFood.Persistence;

namespace ZFood.Tests
{
    public class IntegrationTestsWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();
                services.AddDbContext<ZFoodDbContext>(o =>
                {
                    o.UseInMemoryDatabase("zfood_tests");
                    o.UseInternalServiceProvider(serviceProvider);
                    // Use NoTracking by default
                    // https://docs.microsoft.com/en-us/ef/core/querying/tracking#no-tracking-queries
                    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

                var provider = services.BuildServiceProvider();
                using (var scope = provider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<ZFoodDbContext>();

                    // Ensure the database is created.
                    dbContext.Database.EnsureCreated();
                }
            });
        }
    }
}

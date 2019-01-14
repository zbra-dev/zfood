using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using ZFood.Core;
using ZFood.Core.API;
using ZFood.Core.Decorator;
using ZFood.Core.Validators;
using ZFood.Persistence;
using ZFood.Persistence.API;
using ZFood.Web.Filter;

namespace ZFood.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ZFoodDbContext>(o =>
            {
                o.UseInMemoryDatabase("ZFood_Dev");
                // Use NoTracking by default
                // https://docs.microsoft.com/en-us/ef/core/querying/tracking#no-tracking-queries
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ZFood API",
                    Version = "v1",
                    Description = "ZFood Web API"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Persistence
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVisitRepository, VisitRepository>();

            // Factory
            services.AddTransient<IRestaurantValidatorFactory, RestaurantValidatorFactory>();
            services.AddTransient<IVisitValidatorFactory, VisitValidatorFactory>();
            services.AddTransient<IUserValidatorFactory, UserValidatorFactory>();

            // API
            services.AddTransient<IRestaurantService>(provider =>
            {
                var restaurantRepository = provider.GetService<IRestaurantRepository>();
                var factory = provider.GetService<IRestaurantValidatorFactory>();
                var service = new RestaurantService(restaurantRepository);
                return new RestaurantValidatorDecorator(service, factory);
            });
            services.AddTransient<IVisitService>(provider =>
            {
                var visitRepository = provider.GetService<IVisitRepository>();
                var restaurantRepository = provider.GetService<IRestaurantRepository>();
                var userRepository = provider.GetService<IUserRepository>();
                var factory = provider.GetService<IVisitValidatorFactory>();
                var service = new VisitService(visitRepository, restaurantRepository, userRepository);
                return new VisitValidatorDecorator(service, factory);
            });
            services.AddTransient<IUserService>(provider =>
            {
                var userRepository = provider.GetService<IUserRepository>();
                var factory = provider.GetService<IUserValidatorFactory>();
                var service = new UserService(userRepository);
                return new UserValidatorDecorator(service, factory);
            });

            // Filter
            services.AddMvc(config =>
            {
                config.Filters.Add(new ValidateModelFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ZFoodDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            dbContext.Database.EnsureCreated();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZFood API v1");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

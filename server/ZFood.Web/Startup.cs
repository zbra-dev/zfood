using log4net;
using log4net.Config;
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
using System.Xml;
using ZFood.Core;
using ZFood.Core.API;
using ZFood.Core.Decorator;
using ZFood.Core.Validators;
using ZFood.Persistence;
using ZFood.Persistence.API;
using ZFood.Web.Configuration;
using ZFood.Web.Factory;
using ZFood.Web.Filter;

namespace ZFood.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("LogSettings.config"));

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, log4netConfig["log4net"]);

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
            services.AddTransient<IPageRequestValidatorFactory, PageRequestValidatorFactory>();

            // API
            services.AddTransient<IRestaurantService>(provider =>
            {
                var restaurantRepository = provider.GetService<IRestaurantRepository>();
                var repositoryProxy = LoggerAdviceFactory<IRestaurantRepository>.CreateLoggerAdvice(restaurantRepository);
                var service = new RestaurantService(repositoryProxy);
                var serviceProxy = LoggerAdviceFactory<IRestaurantService>.CreateLoggerAdvice(service);
                var restaurantValidatorFactory = provider.GetService<IRestaurantValidatorFactory>();
                var restaurantValidatorFactoryProxy = LoggerAdviceFactory<IRestaurantValidatorFactory>.CreateLoggerAdvice(restaurantValidatorFactory);
                var pageFactory = provider.GetService<IPageRequestValidatorFactory>();
                var pageFactoryProxy = LoggerAdviceFactory<IPageRequestValidatorFactory>.CreateLoggerAdvice(pageFactory);
                var decorator = new RestaurantValidatorDecorator(serviceProxy, restaurantValidatorFactoryProxy, pageFactoryProxy);                
                return LoggerAdviceFactory<IRestaurantService>.CreateLoggerAdvice(decorator);
            });
            services.AddTransient<IVisitService>(provider =>
            {
                var visitRepository = provider.GetService<IVisitRepository>();
                var visitRepositoryProxy = LoggerAdviceFactory<IVisitRepository>.CreateLoggerAdvice(visitRepository);
                var restaurantRepository = provider.GetService<IRestaurantRepository>();
                var restaurantRepositoryProxy = LoggerAdviceFactory<IRestaurantRepository>.CreateLoggerAdvice(restaurantRepository);
                var userRepository = provider.GetService<IUserRepository>();
                var userRepositoryProxy = LoggerAdviceFactory<IUserRepository>.CreateLoggerAdvice(userRepository);
                var service = new VisitService(visitRepositoryProxy, restaurantRepositoryProxy, userRepositoryProxy);
                var serviceProxy = LoggerAdviceFactory<IVisitService>.CreateLoggerAdvice(service);
                var visitValidatorFactory = provider.GetService<IVisitValidatorFactory>();
                var pageValidatorFactory = provider.GetService<IPageRequestValidatorFactory>();
                var decorator = new VisitValidatorDecorator(serviceProxy, visitValidatorFactory, pageValidatorFactory);
                return LoggerAdviceFactory<IVisitService>.CreateLoggerAdvice(decorator);
            });
            services.AddTransient<IUserService>(provider =>
            {
                var userRepository = provider.GetService<IUserRepository>();
                var userRepositoryProxy = LoggerAdviceFactory<IUserRepository>.CreateLoggerAdvice(userRepository);
                var service = new UserService(userRepositoryProxy);
                var serviceProxy = LoggerAdviceFactory<IUserService>.CreateLoggerAdvice(service);
                var userValidatorFactory = provider.GetService<IUserValidatorFactory>();
                var pageValidatorFactory = provider.GetService<IPageRequestValidatorFactory>();
                var decorator = new UserValidatorDecorator(serviceProxy, userValidatorFactory, pageValidatorFactory);
                return LoggerAdviceFactory<IUserService>.CreateLoggerAdvice(decorator);
            });

            // Configuration
            services.Add(new ServiceDescriptor(typeof(SlackConfiguration), provider => GetSlackConfiguration(), ServiceLifetime.Singleton));

            // Filter
            services.AddMvc(config =>
            {
                config.Filters.Add(new ValidateModelFilter());
                config.Filters.Add(new ExceptionFilter());
            });
        }

        private SlackConfiguration GetSlackConfiguration()
        {
            return Configuration.GetSection("Slack").Get<SlackConfiguration>();
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

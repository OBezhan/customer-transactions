using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OBezhan.CustomerService.API.Infrastructure.Validation;
using System.Reflection;
using OBezhan.CustomerService.API.Data;
using OBezhan.CustomerService.API.Infrastructure.Persistent;
using Swashbuckle.AspNetCore.Swagger;

namespace OBezhan.CustomerService.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc()
                .AddCustomMediatr()
                .AddCustomDbContext(_configuration)
                .AddCustomSwagger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCustomSwagger();
        }
    }

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonFormatters()
                .AddApiExplorer()
                .AddValidation();

            return serviceCollection;
        }

        public static IMvcCoreBuilder AddValidation(this IMvcCoreBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromEntryAssembly()
                    .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            return builder;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();
            serviceCollection.Configure<DatabaseOptions>(configuration.GetSection("Database"));
            serviceCollection.AddDbContext<CustomersDbContext>();
            return serviceCollection;
        }

        public static IServiceCollection AddCustomMediatr(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetEntryAssembly());
            return serviceCollection;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(t =>
            {
                t.SwaggerDoc("v1", new Info
                {
                    Title = "Customers API",
                    Version = "v1"
                });
            });
            return serviceCollection;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(t => { t.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers API"); });
            return appBuilder;
        }
    }
}

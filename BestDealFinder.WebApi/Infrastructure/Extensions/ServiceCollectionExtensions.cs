using BestDealFinder.Models;
using BestDealFinder.Services;
using BestDealFinder.Services.Contracts;
using BestDealFinder.Services.ShippingProviders;
using BestDealFinder.WebApi.Infrastructure.Routes;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace BestDealFinder.WebApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureSections(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<List<ShippingProviderApiDetail>>(
                options => configuration.GetSection("ShippingProviderApiDetails").Bind(options));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IApiClientService, ApiClientService>();

            services.AddScoped<IShippingService, ShippingService>();

            // register shipping providers 
            services.AddScoped<IShippingProvider, AramexProviderService>();
            services.AddScoped<IShippingProvider, BlueDartProviderService>();
            services.AddScoped<IShippingProvider, DelhiveryProviderService>();
            services.AddScoped<IShippingProvider, DHLProviderService>();
            services.AddScoped<IShippingProvider, DTDCProviderService>();
            services.AddScoped<IShippingProvider, EComProviderService>();
            services.AddScoped<IShippingProvider, FedExProviderService>();
            services.AddScoped<IShippingProvider, GATIProviderService>();
            services.AddScoped<IShippingProvider, SafeExpressProviderService>();
        }

        public static void ConfigureRouteConstraints(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("shippingProviders", typeof(ShippingProviderRouteConstraint));
            });
        }
    }
}
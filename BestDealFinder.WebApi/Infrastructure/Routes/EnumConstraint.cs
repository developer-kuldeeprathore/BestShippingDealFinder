using BestDealFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace BestDealFinder.WebApi.Infrastructure.Routes
{
    public class ShippingProviderRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Enum.TryParse(values[routeKey]?.ToString(), out ShippingProviders providers);
        }
    }
}
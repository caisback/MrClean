using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MrClean.Core.Application.Services.Authentication;

namespace MrClean.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            // Old as Services approach:
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // CCQRS via Mediatr approach:
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}

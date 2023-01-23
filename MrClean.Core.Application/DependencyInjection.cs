using Microsoft.Extensions.DependencyInjection;
using MrClean.Core.Application.Services.Authentication;

namespace MrClean.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using MrClean.Core.Application.Common.Interfaces.Authentication;
using MrClean.Core.Application.Common.Interfaces.Persistence;
using MrClean.Core.Application.Common.Interfaces.Providers;
using MrClean.Infrastructure.Authentication;
using MrClean.Infrastructure.Persistence;
using MrClean.Infrastructure.Providers;

namespace MrClean.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            Microsoft.Extensions.Configuration.ConfigurationManager configurationManager) 
        {
            services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}

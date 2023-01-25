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
            services.AddSingleton<ICosmosClientService>(new CosmosClientService("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="));
            return services;
        }
    }
}

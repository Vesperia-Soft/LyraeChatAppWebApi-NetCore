using LyraeChatApp.Application.Services;
using LyraeChatApp.Infrastructure.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LyraeChatApp.Presentation.Configurations.Extensions;

public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection JwtServiceCollections(this IServiceCollection services)
    {
      
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}

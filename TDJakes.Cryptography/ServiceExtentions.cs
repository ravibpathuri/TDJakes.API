using Microsoft.Extensions.DependencyInjection;

namespace TDJakes.Cryptography;
public static class ServiceExtentions
{
    public static void AddTDJakesCryptography(this IServiceCollection services)
    {
        services.AddTransient<IJwtUtils, JwtUtils>();
    }
}
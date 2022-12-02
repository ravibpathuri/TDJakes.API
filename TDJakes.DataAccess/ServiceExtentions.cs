using Microsoft.Extensions.DependencyInjection;

namespace TDJakes.DataAccess;

public static class ServiceExtentions
{
    public static void AddTDJakesDataAccess(this IServiceCollection services)
    {
        services.AddTransient<IDbAccess, MySqlDbAccess>();
    }
}

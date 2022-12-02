using Microsoft.Extensions.DependencyInjection;
using TDJakes.DataAccess;

namespace TDJakes.Business;

public static class ServiceExtentions
{
    public static void AddTDJakesBusiness(this IServiceCollection services)
    {
        services.AddTDJakesDataAccess();
        services.AddTransient<IUserRepo, UserRepo>();
    }
}

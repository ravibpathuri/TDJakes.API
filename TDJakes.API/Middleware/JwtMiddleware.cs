using Microsoft.Extensions.Options;
using TDJakes.Business;
using TDJakes.Cryptography;
using TDJakes.Models.AppSettings;

namespace TDJakes.API.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Jwt _jwtAppSettings;
    public JwtMiddleware(RequestDelegate next, IOptions<Jwt> appSettings)
    {
        _next = next;
        _jwtAppSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserRepo userRepo, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        string userId = jwtUtils.ValidateJwtToken(token ?? "");
        if (!string.IsNullOrEmpty(userId))
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = await userRepo.GetUserByEmail(userId);
        }

        await _next(context);
    }
}
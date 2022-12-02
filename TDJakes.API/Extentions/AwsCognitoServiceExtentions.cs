using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TDJakes.API.Extentions;
public static class AwsCognitoServiceExtentions
{
    public static void AddAwsCognito(this IServiceCollection services, IConfiguration configuration)
    {
        // var d = configuration.GetSection("jwt").GetValue<string>("");

        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme, "AzureAD");
            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

            // Add Policy, leave this section if we dont want to use role based authorization
            options.AddPolicy("<TDJakesGroup:admin>", policy => policy.Requirements.Add(new CognitoGroupAuthorizationRequirement("<TDJakesGroup:admin>")));
            options.AddPolicy("<TDJakesGroup:user>", policy => policy.Requirements.Add(new CognitoGroupAuthorizationRequirement("<TDJakesGroup:user>")));
            options.AddPolicy("<TDJakesGroup:employer>", policy => policy.Requirements.Add(new CognitoGroupAuthorizationRequirement("<TDJakesGroup:employer>")));
            options.AddPolicy("<TDJakesGroup:coach>", policy => policy.Requirements.Add(new CognitoGroupAuthorizationRequirement("<TDJakesGroup:coach>")));
        });

        services.AddSingleton<IAuthorizationHandler, CognitoGroupAuthorizationHandler>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Audience = "<APP_ID you copied from Coginoto earlier>";
            options.Authority = "https://cognito-idp.<region>.amazonaws.com/<User Pool Id>";
            options.RequireHttpsMetadata = false;
        });
    }
}
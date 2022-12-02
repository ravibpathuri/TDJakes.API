using Microsoft.OpenApi.Models;
using TDJakes.API.Extentions;
using TDJakes.Business;
using TDJakes.Cryptography;
using TDJakes.Models.AppSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TDJakes API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer",
            }
            },
            new string[] { }
        }
    });
});

// TD Jakes related services
builder.Services.AddAwsCognito(builder.Configuration);
builder.Services.AddTDJakesBusiness();
builder.Services.AddTDJakesCryptography();
builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// TDJakes related middleware
// we dont need this as we used AWS Congito
//app.UseMiddleware<JwtMiddleware>();

app.Run();

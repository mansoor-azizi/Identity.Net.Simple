using Identity.Net.Simple.Database;
using Identity.Net.Simple.Models.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Web.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityNetSimple(
    this IServiceCollection services,
    IConfiguration configuration) =>
    services.AddDatabase(configuration)
            .AddIdentityFeature(configuration)
            .AddJwtTokenConfigs(configuration)
            .AddSwaggerGenWithAuth();

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>(
                options => options.UseSqlite(connectionString));

        return services;
    }

    private static IServiceCollection AddIdentityFeature(this IServiceCollection services, IConfiguration configuration)
    {
        // Add IdentityCore

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = false;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            // uncomment if you want to use default identity apis
            //.AddApiEndpoints()
            .AddDefaultTokenProviders();

        return services;
    }


    private static IServiceCollection AddJwtTokenConfigs(this IServiceCollection services, IConfiguration configuration)
    {

        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = jwtSettings["Issuer"],
                  ValidAudience = jwtSettings["Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(key)
              };
          });

        return services;
    }

    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            };

            o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            };

            o.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}

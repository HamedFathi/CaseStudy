using System.Text;
using CaseStudy.Domain.Identity;
using HamedStack.TheAggregateRoot.Events;
using HamedStack.TheRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CaseStudy.Domain.Identity.Models;
using CaseStudy.Infrastructure.Identity;
using CaseStudy.Infrastructure.Identity.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace CaseStudy.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure<TDbContext>(this IServiceCollection services, Action<JsonWebTokenOption> jwtOption)
        where TDbContext : IdentityDbContextBase
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<TDbContext>();
        services.AddScoped<IdentityDbContextBase>(provider => provider.GetRequiredService<TDbContext>());
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TDbContext>());
        services.AddScoped(typeof(IRepository<>), typeof(IdentityRepository<>));

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TDbContext>()
            .AddDefaultTokenProviders();
        var jwtConfig = new JsonWebTokenOption();
        jwtOption(jwtConfig);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = jwtConfig.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = jwtConfig.ValidAudience,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SigningKey))
                };
            });

        services.AddSingleton(jwtConfig);
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();

        return services;
    }
}
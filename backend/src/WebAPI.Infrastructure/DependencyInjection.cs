using System.Text;
using Admin2022Soft1.Infrastructure.File;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Application.Configuration;
using WebAPI.Application.Common.Interface;
using WebAPI.Infrastructure.Identity;
using WebAPI.Infrastructure.Persistence;
using WebAPI.Infrastructure.Persistence.EfRepository;

namespace WebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebAPIDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("PostgreSQL"),
                    b => b.MigrationsAssembly(typeof(WebAPIDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<IAppFileUploadService, AppFileUploadService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false;//配置是否为https协议
                    option.SaveToken = true;//配置token是否保存在api上下文

                    var tokenParameter = configuration.GetSection("JwtSetting").Get<JwtSetting>();
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = tokenParameter.Issuer,
                        ValidAudience = tokenParameter.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameter.Secret))
                    };
                });
            return services;
        }
    }
}
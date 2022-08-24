using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Common.Interface;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Persistence;

namespace ApiDemo.Infrastructure
{
    public static class ApplicationStartupExtensions
    {
        public static async void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<WebAPIDbContext>();

                // 自动同步迁移文件到数据库服务器，前提是必须先（手动）生成迁移文件
                context.Database.Migrate();

                var appUserRes = services.GetRequiredService<IRepository<AppUser>>();
                var appRoleRes = services.GetRequiredService<IRepository<AppRole>>();
                var appUserRoleRes = services.GetRequiredService<IRepository<AppUserRole>>();
                var appArticleClass = services.GetRequiredService<IRepository<AppArticleClass>>();

                if (!appArticleClass.Table.Any())
                {
                    List<AppArticleClass> appArticleClasses = new List<AppArticleClass>();

                    appArticleClasses.Add(new AppArticleClass
                    {
                        AppArticleClassName = "c"
                    });
                    appArticleClasses.Add(new AppArticleClass
                    {
                        AppArticleClassName = "c#"
                    });
                    appArticleClasses.Add(new AppArticleClass
                    {
                        AppArticleClassName = "java"
                    });
                    appArticleClasses.Add(new AppArticleClass
                    {
                        AppArticleClassName = "php"
                    });

                    await appArticleClass.AddBulkAsync(appArticleClasses);
                }

                // 如果没有任何角色，则生成种子数据
                if (!appRoleRes.Table.Any())
                {
                    var role = new AppRole
                    {
                        RoleName = "超级管理员"
                    };
                    var user = new AppUser
                    {
                        Username = "admin",
                        Password = "123456"
                    };
                    await appRoleRes.AddAsync(role);
                    await appUserRes.AddAsync(user);
                    await appUserRoleRes.AddAsync(new AppUserRole
                    {
                        AppRoleId = role.Id,
                        AppUserId = user.Id
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred migrating the DB: {ex.Message}");
            }
        }
    }
}
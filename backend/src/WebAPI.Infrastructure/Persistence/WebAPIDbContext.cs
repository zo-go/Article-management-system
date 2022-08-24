using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entity;

namespace WebAPI.Infrastructure.Persistence
{
    public class WebAPIDbContext : DbContext
    {
        public WebAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public WebAPIDbContext() : base()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 应用当前Assembly中定义的所有的Configurations，就不需要一个一个去写了。
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> AppUser => Set<AppUser>();
        public DbSet<AppRole> AppRole => Set<AppRole>();
        public DbSet<AppUserRole> AppUserRole => Set<AppUserRole>();
        public DbSet<AppAuditLog> AppAuditLog => Set<AppAuditLog>();
        public DbSet<AppExceptionLog> AppExceptionLog => Set<AppExceptionLog>();
        public DbSet<AppIdentityUser> AppIdentityUser => Set<AppIdentityUser>();
        public DbSet<AppFileInformation> AppFileInformation => Set<AppFileInformation>();
        public DbSet<AppArticle> AppArticle => Set<AppArticle>();
        public DbSet<AppArticleClass> AppArticleClass => Set<AppArticleClass>();
        // public DbSet<AppGiveALike> AppGiveALike => Set<AppGiveALike>();

    }
}
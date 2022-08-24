using ApiDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI.API.Service;
using WebAPI.Application.Common.Interface;
using WebAPI.Infrastructure;
using WebAPI.Infrastructure.Fileter;
using WebAPI.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});
builder.Services.AddControllers().AddNewtonsoftJson(option =>
                //忽略循环引用
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

// 注册请求响应的上下文
builder.Services.AddHttpContextAccessor();

// 注册当前传话用户服务
builder.Services.AddTransient<ISessionUserService, SessionUserService>();
// Add services to the container.

// 注册基础设施层中的所有实例
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(typeof(HttpGlobalExceptionFilter));
    opt.Filters.Add(typeof(AuditLogActionFilter));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

// token认证
app.UseAuthentication();
// token授权
app.UseAuthorization();

app.MapControllers();

// 调用扩展方法
app.MigrateDatabase();

app.Run();

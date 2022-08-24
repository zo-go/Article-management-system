using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Application.Common.Interface;
using WebAPI.Domain.Entity;
using WebAPI.Infrastructure.Utils;

namespace WebAPI.Infrastructure.Fileter
{
    // 全局保存操作日志到审计日志表
    public class AuditLogActionFilter : IAsyncActionFilter
    {
        private readonly IRepository<AppAuditLog> _appAuditLogRes;
        public AuditLogActionFilter(
            IRepository<AppAuditLog> appAuditLogRes
            )
        {
            _appAuditLogRes = appAuditLogRes;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 判断是否写日志
            if (!ShouldSaveAudit(context))
            {
                await next();
                return;
            }
            //接口Type
            var type = (context.ActionDescriptor as ControllerActionDescriptor)?.ControllerTypeInfo.AsType();
            //方法信息
            var method = (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo;
            //方法参数
            var arguments = context.ActionArguments;
            //开始计时
            var stopwatch = Stopwatch.StartNew();
            var auditInfo = new AppAuditLog
            {
                UserInfo = "匿名",
                ServiceName = type != null ? type.FullName : "",
                MethodName = method?.Name,
                ////请求参数转Json
                Parameters = JsonHelper.SerializeObject(arguments),
                ExecutionTime = DateTime.UtcNow,
                BrowserInfo = context.HttpContext.Request.Headers["User-Agent"],
                ClientIpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                //ClientName = _clientInfoProvider.ComputerName.TruncateWithPostfix(EntityDefault.FieldsLength100),
                // Id = Guid.NewGuid().ToString()
            };

            ActionExecutedContext? result = null;
            try
            {
                result = await next();
                if (result.Exception != null && !result.ExceptionHandled)
                {
                    auditInfo.Exception = result.Exception.ToString();
                }
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex.ToString();
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

                if (result != null)
                {
                    switch (result.Result)
                    {
                        case ObjectResult objectResult:
                            auditInfo.ReturnValue = JsonHelper.SerializeObject(objectResult.Value);
                            break;

                        case JsonResult jsonResult:
                            auditInfo.ReturnValue = JsonHelper.SerializeObject(jsonResult.Value);
                            break;

                        case ContentResult contentResult:
                            auditInfo.ReturnValue = contentResult.Content;
                            break;
                    }
                }
                // Console.WriteLine(auditInfo.ToString());
                //保存审计日志
                await _appAuditLogRes.AddAsync(auditInfo);
            }
        }

        /// <summary>
        /// 是否需要记录审计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ShouldSaveAudit(ActionExecutingContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
                return false;
            var methodInfo = (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo;

            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            // if (methodInfo.GetCustomAttribute<AuditedAttribute>() != null)
            // {
            //     return true;
            // }

            // if (methodInfo.GetCustomAttribute<DisableAuditingAttribute>() != null)
            // {
            //     return false;
            // }

            // var classType = methodInfo.DeclaringType;
            // if (classType != null)
            // {
            //     if (classType.GetTypeInfo().GetCustomAttribute<AuditedAttribute>() != null)
            //     {
            //         return true;
            //     }

            //     if (classType.GetTypeInfo().GetCustomAttribute<AuditedAttribute>() != null)
            //     {
            //         return false;
            //     }
            // }
            return true;
        }
    }
}
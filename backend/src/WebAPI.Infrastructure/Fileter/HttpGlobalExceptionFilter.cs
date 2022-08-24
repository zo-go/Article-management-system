using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Application.Common.Interface;
using WebAPI.Domain.Entity;

namespace WebAPI.Infrastructure.Fileter
{
    public class HttpGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IRepository<AppExceptionLog> _appExceptionLogRes;

        public HttpGlobalExceptionFilter(IRepository<AppExceptionLog> appExceptionLogRes)
        {
            _appExceptionLogRes = appExceptionLogRes;
        }


        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //捕获全局异常，记入数据库日志
            await _appExceptionLogRes.AddAsync(new AppExceptionLog
            {
                ShortMessage = context.Exception.Message,
                FullMessage = context.Exception.ToString()
            });
            context.ExceptionHandled = true;
        }
    }
}
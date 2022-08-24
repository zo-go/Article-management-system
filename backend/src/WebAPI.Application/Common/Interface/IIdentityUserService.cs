using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Entity;

namespace WebAPI.Application.Common.Interface
{
    public interface IIdentityUserService
    {
        AppIdentityUser? GetIdentityUserByUserNameAsync(string username);
        Task<bool> ValidatePassword(string username, string Password);
        Task UpdateAsync(AppIdentityUser appIdentityUser);
    }
}
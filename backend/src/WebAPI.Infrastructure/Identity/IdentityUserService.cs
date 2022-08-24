// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using WebAPI.Application.Common.Interface;
// using WebAPI.Domain.Entity;

// namespace WebAPI.Infrastructure.Identity
// {
//     public class IdentityUserService : IIdentityUserService
//     {
//         private readonly IRepository<AppIdentityUser> _appIdentityUser;
//         private readonly IRepository<AppUser> _appUser;

//         public IdentityUserService(IRepository<AppIdentityUser> appIdentityUser, IRepository<AppUser> appUser)
//         {
//             _appIdentityUser = appIdentityUser;
//             _appUser = appUser;
//         }
//         public AppIdentityUser? GetIdentityUserByUserNameAsync(string username)
//         {
//             return _appIdentityUser.Table.FirstOrDefault(x => x.Username == username);
//         }

//         public async Task UpdateAsync(AppIdentityUser appIdentityUser)
//         {
//             // 清除全部的RefreshToken记录
//             var tmpUser = _appIdentityUser.Table.Where(x => x.Username == appIdentityUser.Username);
//             await _appIdentityUser.DeleteRangeAsync(tmpUser, CancellationToken.None, true);
//             await _appIdentityUser.AddAsync(appIdentityUser);
//         }

//         public Task<bool> ValidatePassword(string username, string password)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }
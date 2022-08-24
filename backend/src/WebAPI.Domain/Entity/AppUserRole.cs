using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Base;

namespace WebAPI.Domain.Entity
{
    public class AppUserRole : BaseEntity
    {
        public Guid AppUserId { get; set; }
        public Guid AppRoleId { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual AppRole? AppRole { get; set; }
    }
}
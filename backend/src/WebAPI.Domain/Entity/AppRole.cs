using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Base;

namespace WebAPI.Domain.Entity
{
    public class AppRole : BaseEntity
    {
        public string RoleName { get; set; } = null!;
    }
}
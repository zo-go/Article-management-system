using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Base;

namespace WebAPI.Domain.Entity
{
    public class AppExceptionLog : BaseEntity
    {
        public string ShortMessage { get; set; } = null!;
        public string FullMessage { get; set; } = null!;
    }
}
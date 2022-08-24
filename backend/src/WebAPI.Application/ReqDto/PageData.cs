using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Application.ReqDto
{
    public class PageData
    {
        public string? keyword{get;set;}
        public int pageSize{get;set;}
        public int pageIndex{get;set;}
    }
}
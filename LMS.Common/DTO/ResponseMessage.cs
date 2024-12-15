using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Common.DTO
{
    public class ResponseMessage
    {
        public object? ResponseObj { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
    }
}

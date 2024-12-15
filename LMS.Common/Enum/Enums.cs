using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Common.Enum
{
    public class Enums
    {
        public enum ResponseStatusCode
        {
            Success = 1,
            Failed = 2,
            Warning = 3,
            NotFound = 500,
            Info = 300,
            UnAuthorize = 401
        }

    }
}

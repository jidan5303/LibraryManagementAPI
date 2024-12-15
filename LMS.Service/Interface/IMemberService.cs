using LMS.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Interface
{
    public interface IMemberService
    {
        Task<ResponseMessage> GetAllMember(RequestMessage requestMessage);
        Task<ResponseMessage> SaveMember(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteMember(RequestMessage requestMessage);
    }
}

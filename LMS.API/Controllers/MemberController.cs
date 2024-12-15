using LMS.Common.DTO;
using LMS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/Member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost]
        [Route("GetAllMember")]
        public async Task<ResponseMessage> GetAllMember(RequestMessage requestMessage)
        {
            return await _memberService.GetAllMember(requestMessage);
        }

        [HttpPost]
        [Route("SaveMember")]
        public async Task<ResponseMessage> SaveMember(RequestMessage requestMessage)
        {
            return await _memberService.SaveMember(requestMessage);
        }

        [HttpPost]
        [Route("DeleteMember")]
        public async Task<ResponseMessage> DeleteMember(RequestMessage requestMessage)
        {
            return await _memberService.DeleteMember(requestMessage);
        }
    }
}

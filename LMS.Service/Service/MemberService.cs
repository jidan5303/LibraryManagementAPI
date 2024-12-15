using LMS.Common.DTO;
using LMS.Common.Enum;
using LMS.Common.Model;
using LMS.DataAccess;
using LMS.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Service
{
    public class MemberService : IMemberService
    {
        private readonly LMSDbContext _lMSDbContext;

        public MemberService(LMSDbContext lMSDbContext)
        {
            _lMSDbContext = lMSDbContext;
        }

        public async Task<ResponseMessage> DeleteMember(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                int MemberID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
                if(MemberID > 0)
                {
                    var member = await _lMSDbContext.Members.Where(x => x.MemberID == MemberID).FirstOrDefaultAsync();
                    if(member != null)
                    {
                        _lMSDbContext.Members.Remove(member);
                        await _lMSDbContext.SaveChangesAsync();

                        responseMessage.Message = "Member deleted successfully";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                    }
                    else
                    {
                        responseMessage.Message = "Member not found";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.NotFound;
                    }
                }
                else
                {
                    responseMessage.Message = "Invalid Member ID";
                    responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Failed;
                }
            }
            catch (Exception ex)
            {
                responseMessage.Message = ex.Message;
                responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Failed;
            }
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllMember(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                List<Members> lstMember = await _lMSDbContext.Members.ToListAsync();
                if (lstMember.Count > 0)
                {
                    responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                    responseMessage.ResponseObj = lstMember;
                }
                else
                {
                    responseMessage.Message = "No member found";
                    responseMessage.StatusCode = (int)Enums.ResponseStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseMessage.Message = ex.Message;
                responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Failed;
            }
            return responseMessage;
        }

        public async Task<ResponseMessage> SaveMember(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                Members objMember = JsonConvert.DeserializeObject<Members>(requestMessage.RequestObj.ToString());
                if (objMember != null)
                {
                    if (objMember.MemberID > 0)
                    {
                        var existMember = await _lMSDbContext.Members.AsNoTracking().Where(x => x.MemberID == objMember.MemberID).FirstOrDefaultAsync();
                        if (existMember != null)
                        {
                            _lMSDbContext.Members.Update(objMember);
                           
                            responseMessage.Message = "Member updated successfully";
                            responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                            responseMessage.ResponseObj = objMember;
                        }
                        else
                        {
                            responseMessage.Message = "Member not found";
                            responseMessage.StatusCode = (int)Enums.ResponseStatusCode.NotFound;
                        }
                    }
                    else
                    {

                        _lMSDbContext.Members.Add(objMember);
                        await _lMSDbContext.SaveChangesAsync();

                        responseMessage.Message = "Member added successfully";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                        responseMessage.ResponseObj = objMember;
                    }
                }
                else
                {
                    responseMessage.Message = "Invalid member data";
                    responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Failed;
                }
            }
            catch (Exception ex)
            {
                responseMessage.Message = ex.Message;
                responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Failed;
            }
            return responseMessage;
        }
    }
}

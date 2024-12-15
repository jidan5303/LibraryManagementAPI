using LMS.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Interface
{
    public interface IBookService
    {
        Task<ResponseMessage> GetAllBook(RequestMessage requestMessage);
        Task<ResponseMessage> SaveBook(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteBook(RequestMessage requestMessage);
        Task<ResponseMessage> GetAllBorrowedBooksRecord(RequestMessage requestMessage);
        Task<ResponseMessage> SaveBorrowedBookRecord(RequestMessage requestMessage);
        Task<ResponseMessage> DeleteBorrowedBookRecord(RequestMessage requestMessage);
        Task<ResponseMessage> MarkAsReturn(RequestMessage requestMessage);
    }
}

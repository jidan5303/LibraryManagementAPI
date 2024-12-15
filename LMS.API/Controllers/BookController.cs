using LMS.Common.DTO;
using LMS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("GetAllBook")]
        public async Task<ResponseMessage> GetAllBook(RequestMessage requestMessage)
        {
            return await _bookService.GetAllBook(requestMessage);
        }

        [HttpPost]
        [Route("SaveBook")]
        public async Task<ResponseMessage> SaveBook(RequestMessage requestMessage)
        {
            return await _bookService.SaveBook(requestMessage);
        }

        [HttpPost]
        [Route("DeleteBook")]
        public async Task<ResponseMessage> DeleteBook(RequestMessage requestMessage)
        {
            return await _bookService.DeleteBook(requestMessage);
        }

        [HttpPost]
        [Route("GetAllBorrowedBooksRecord")]
        public async Task<ResponseMessage> GetAllBorrowedBooksRecord(RequestMessage requestMessage)
        {
            return await _bookService.GetAllBorrowedBooksRecord(requestMessage);
        }

        [HttpPost]
        [Route("SaveBorrowedBookRecord")]
        public async Task<ResponseMessage> SaveBorrowedBookRecord(RequestMessage requestMessage)
        {
            return await _bookService.SaveBorrowedBookRecord(requestMessage);
        }

        [HttpPost]
        [Route("DeleteBorrowedBookRecord")]
        public async Task<ResponseMessage> DeleteBorrowedBookRecord(RequestMessage requestMessage)
        {
            return await _bookService.DeleteBorrowedBookRecord(requestMessage);
        }

        [HttpPost]
        [Route("MarkAsReturn")]
        public async Task<ResponseMessage> MarkAsReturn(RequestMessage requestMessage)
        {
            return await _bookService.MarkAsReturn(requestMessage);
        }
    }
}

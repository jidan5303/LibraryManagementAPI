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
    public class BookService : IBookService
    {
        private readonly LMSDbContext _lMSDbContext;
        public BookService(LMSDbContext lMSDbContext)
        {
            _lMSDbContext = lMSDbContext;
        }
        public async Task<ResponseMessage> DeleteBook(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                int bookID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());

                if (bookID > 0)
                {
                    var book = await _lMSDbContext.Books.Where(x => x.BookID == bookID).FirstOrDefaultAsync();
                    if (book != null)
                    {
                        _lMSDbContext.Books.Remove(book);
                        await _lMSDbContext.SaveChangesAsync();

                        responseMessage.Message = "Book deleted successfully";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                    }
                    else
                    {
                        responseMessage.Message = "Book not found";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.NotFound;
                    }
                }
                else
                {
                    responseMessage.Message = "Invalid book ID";
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

        public async Task<ResponseMessage> GetAllBook(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                List<Books> lstBook = await _lMSDbContext.Books.ToListAsync();
                if (lstBook.Count > 0)
                {
                    responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                    responseMessage.ResponseObj = lstBook;
                }
                else
                {
                    responseMessage.Message = "Books not found";
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

        public async Task<ResponseMessage> SaveBook(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                Books objBook = JsonConvert.DeserializeObject<Books>(requestMessage.RequestObj.ToString());
                if (objBook != null)
                {
                    if (objBook.BookID > 0)
                    {
                        var existBook = await _lMSDbContext.Books.AsNoTracking().Where(x => x.BookID == objBook.BookID).FirstOrDefaultAsync();
                        if (existBook != null)
                        {
                            _lMSDbContext.Books.Update(objBook);
                            await _lMSDbContext.SaveChangesAsync();

                            responseMessage.Message = "Book updated successfully";
                            responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                            responseMessage.ResponseObj = objBook;
                        }
                        else
                        {
                            responseMessage.Message = "Book not found";
                            responseMessage.StatusCode = (int)Enums.ResponseStatusCode.NotFound;
                        }
                    }
                    else
                    {
                        _lMSDbContext.Books.Add(objBook);
                        await _lMSDbContext.SaveChangesAsync();

                        responseMessage.Message = "Book added successfully";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                        responseMessage.ResponseObj = objBook;
                    }
                }
                else
                {
                    responseMessage.Message = "Invalid book data";
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

        public async Task<ResponseMessage> GetAllBorrowedBooksRecord(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                List<BorrowedBooks> lstBorrowedBookRecord = await _lMSDbContext.BorrowedBooks.ToListAsync();
                if (lstBorrowedBookRecord.Count > 0)
                {
                    foreach (var item in lstBorrowedBookRecord)
                    {
                        item.objBook = await _lMSDbContext.Books.Where(x => x.BookID == item.BookID).FirstOrDefaultAsync();
                        item.objMember = await _lMSDbContext.Members.Where(x => x.MemberID == item.MemberID).FirstOrDefaultAsync();
                    }

                    responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                    responseMessage.ResponseObj = lstBorrowedBookRecord;
                }
                else
                {
                    responseMessage.Message = "Borrowed book record not found";
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

        public async Task<ResponseMessage> SaveBorrowedBookRecord(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                BorrowedBooks objBorrowedBook = JsonConvert.DeserializeObject<BorrowedBooks>(requestMessage.RequestObj.ToString());
                if (objBorrowedBook != null)
                {
                    if (objBorrowedBook.BookID > 0)
                    {
                        var existBorrowedBook = await _lMSDbContext.BorrowedBooks.AsNoTracking().Where(x => x.BookID == objBorrowedBook.BookID).FirstOrDefaultAsync();
                        if (existBorrowedBook != null)
                        {
                            responseMessage.Message = "This book is already borrowed";
                            responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Failed;
                            responseMessage.ResponseObj = objBorrowedBook;
                        }
                        else
                        {
                            objBorrowedBook.ReturnDate = objBorrowedBook.BorrowDate.AddDays(14);
                            objBorrowedBook.IsReturned = false;
                            _lMSDbContext.BorrowedBooks.Add(objBorrowedBook);
                            await _lMSDbContext.SaveChangesAsync();

                            responseMessage.Message = "Borrowed book record added successfully";
                            responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                            responseMessage.ResponseObj = objBorrowedBook;
                        }
                    }
                }
                else
                {
                    responseMessage.Message = "Invalid borrowed book record data";
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

        public async Task<ResponseMessage> DeleteBorrowedBookRecord(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                int borrowID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());

                if (borrowID > 0)
                {
                    var borrowedBook = await _lMSDbContext.BorrowedBooks.Where(x => x.BorrowID == borrowID).FirstOrDefaultAsync();
                    if (borrowedBook != null)
                    {
                        _lMSDbContext.BorrowedBooks.Remove(borrowedBook);
                        await _lMSDbContext.SaveChangesAsync();

                        responseMessage.Message = "Borrowed book record deleted successfully";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                    }
                    else
                    {
                        responseMessage.Message = "Borrowed book record not found";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.NotFound;
                    }
                }
                else
                {
                    responseMessage.Message = "Invalid borrowed book record ID";
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

        public async Task<ResponseMessage> MarkAsReturn(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                int borrowID = JsonConvert.DeserializeObject<int>(requestMessage.RequestObj.ToString());
                if(borrowID > 0)
                {
                    var existBorrowedBook = await _lMSDbContext.BorrowedBooks.Where(x => x.BorrowID == borrowID).FirstOrDefaultAsync();
                    if(existBorrowedBook != null)
                    {
                        existBorrowedBook.ReturnDate = DateTime.Now;
                        existBorrowedBook.IsReturned = true;
                        _lMSDbContext.BorrowedBooks.Update(existBorrowedBook);
                        await _lMSDbContext.SaveChangesAsync();

                        responseMessage.Message = "Book marked as return successfully";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.Success;
                    }
                    else
                    {
                        responseMessage.Message = "Borrowed book record not found";
                        responseMessage.StatusCode = (int)Enums.ResponseStatusCode.NotFound;
                    }
                }
                else
                {
                    responseMessage.Message = "Invalid borrowed book record ID";
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

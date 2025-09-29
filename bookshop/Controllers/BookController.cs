using bookshop.DataAccessLayer;
using bookshop.DataAccessLayer.Models;
using bookshop.DataAccessLayer.Models.DAO;
using bookshop.DataAccessLayer.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private BookShopContext _context;
        private BookDAO bookDAO;

        public BookController(ILogger<BookController> logger, BookShopContext context, BookDAO bookDAO)
        {
            _logger = logger;
            _context = context;
            this.bookDAO = bookDAO;
        }

        [HttpGet("/getallbooks")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<List<BookListData>> GetAllBooks()
        {
            return await bookDAO.GetAll();
        }

        [HttpGet("/getbook/{id}")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<BookDetail> GetBook([FromRoute] int id)
        {
            //return _context.Book.Find(id);
            return await bookDAO.GetOneBook(id);
        }

        [HttpPost("/addbook")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<bool>? AddBook([FromBody] Book book)
        {

            //handle error , use try catch
            //var result = await _context.Book.AddAsync(book);
            //await _context.SaveChangesAsync();
            
            return await bookDAO.Add(book);
        }



        [HttpDelete("/deletebook/{id}")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id )
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Book Id is required.");
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound($"Book with id {id} not found.");
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return Ok($"Book with id {id} deleted successfully.");
        }

        [HttpPut("/updatebook")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBook updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("Book Id is required.");
            }

            var result = await bookDAO.UpdateBook(updatedBook);
            return Ok($"Book with id {updatedBook.ID} updated successfully.");
        }

    }
}

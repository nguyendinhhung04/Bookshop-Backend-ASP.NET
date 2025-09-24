using bookshop.DataAccessLayer;
using bookshop.DataAccessLayer.Models;
using bookshop.DataAccessLayer.Models.DAO;
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
        private IDAO<Book> bookDAO;

        public BookController(ILogger<BookController> logger, BookShopContext context, IDAO<Book> bookDAO)
        {
            _logger = logger;
            _context = context;
            this.bookDAO = bookDAO;
        }

        [HttpGet("/getallbooks")]
        [EnableCors("MyAllowSpecificOrigins")]
        public List<Book> GetAllBooks()
        {
            //return _context.Book.ToList<Book>();
            return bookDAO.GetAll();

        }

        [HttpGet("/getbook/{id}")]
        [EnableCors("MyAllowSpecificOrigins")]
        public Book? GetBook([FromRoute] int id)
        {
            return _context.Book.Find(id);
        }

        [HttpPost("/addbook")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            //Use Sequence 
            //string  id = Guid.NewGuid().ToString();

            int id = (new Random()).Next(0, 999);
            while ( await _context.Book.FindAsync(id) != null )
            {
                id = (new Random()).Next(0, 999);
            }

            book.ID = id;

            //handle error , use try catch
            var result = await _context.Book.AddAsync(book);

            await _context.SaveChangesAsync();
            return Ok("Saved new book");
        }

        [HttpDelete("/deletebook/{id}")]
        [EnableCors("MyAllowSpecificOrigins")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id )
        {
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
        public async Task<IActionResult> UpdateBook([FromBody] Book updatedBook)
        {
            if (updatedBook.ID == null)
            {
                return BadRequest("Book Id is required.");
            }
            var book = await _context.Book.FindAsync(updatedBook.ID);
            if (book == null)
            {
                return NotFound($"Book with id {updatedBook.ID} not found.");
            }
            // Update properties
            book.NAME = updatedBook.NAME;
            book.NUMBER_OF_PAGE = updatedBook.NUMBER_OF_PAGE;
            book.ON_SALE = updatedBook.ON_SALE;

            await _context.SaveChangesAsync();
            return Ok($"Book with id {updatedBook.ID} updated successfully.");
        }

    }
}

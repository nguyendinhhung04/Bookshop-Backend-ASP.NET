using bookshop.Models;
using bookshop.Models.DAO;
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

        public BookController(ILogger<BookController> logger, BookShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("/getbooks")]
        [EnableCors("MyAllowSpecificOrigins")]
        public List<Book> GetByContext()
        {
            return _context.Book.ToList();
        }

    }
}

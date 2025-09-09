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
        private IDAO<Book> _bookDAO;
        private BookShopContext _context;

        public BookController(ILogger<BookController> logger, IDAO<Book> bookDAO, BookShopContext context)
        {
            _logger = logger;
            _bookDAO = bookDAO;
            _context = context;
        }

        [HttpGet("/getbooks")]
        [EnableCors("MyAllowSpecificOrigins")]
        public IEnumerable<Object> Get()
        {
            return _bookDAO.GetAll().ToArray();
        }

        [HttpGet("/gettemp")]
        [EnableCors("MyAllowSpecificOrigins")]
        public IEnumerable<Object> GetTemp()
        {
            return _bookDAO.GetTempData();
        }

        [HttpGet("/getbycontext")]
        [EnableCors("MyAllowSpecificOrigins")]
        public int GetByContext()
        {
            return _context.Books.Count();
        }

    }
}

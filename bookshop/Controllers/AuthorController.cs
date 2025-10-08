using bookshop.DataAccessLayer;
using bookshop.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthorController
    {
        private readonly ILogger<BookController> _logger;
        private BookShopContext _context;

        public AuthorController(ILogger<BookController> logger, BookShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("/getallauthors")]
        public async Task<List<Author>> GetAllAuthors()
        {
            return _context.Author.ToList();
        }

    }
}

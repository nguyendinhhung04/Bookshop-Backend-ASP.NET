using bookshop.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using bookshop.DataAccessLayer.Models.DAO;

namespace bookshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {

        private readonly ILogger<CategoryController> _logger;
        private CategoryDAO categoryDAO;

        public CategoryController(ILogger<CategoryController> logger, CategoryDAO categoryDAO)
        {
            _logger = logger;
            this.categoryDAO = categoryDAO;
        }

        [HttpGet("/getcategory/{id}")]
        public async Task<Category> GetCategory([FromRoute] int id)
        {
            return await categoryDAO.GetOneCategory(id);
        }

        [HttpGet("/getallcategory")]
        public async Task<List<Category>> GetAllCategory()
        {
            return await categoryDAO.GetAll();
        }

    }
}

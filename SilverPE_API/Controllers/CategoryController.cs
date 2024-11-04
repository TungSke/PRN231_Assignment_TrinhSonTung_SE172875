using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SilverPE_Repository;

namespace SilverPE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo categoryRepo;
        public CategoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categoryRepo.GetCategories());
        }
    }
}

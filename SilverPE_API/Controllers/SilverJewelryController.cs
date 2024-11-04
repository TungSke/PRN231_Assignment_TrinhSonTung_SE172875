using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SilverPE_BOs.Models;
using SilverPE_Repository;

namespace SilverPE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SilverJewelryController : ODataController
    {
        private readonly IJewelryRepo jewelryRepo;
        private readonly IAccountRepo accountRepo;
        private readonly ICategoryRepo categoryRepo;
        public SilverJewelryController(IJewelryRepo jewelryRepo, IAccountRepo accountRepo, ICategoryRepo categoryRepo)
        {
            this.jewelryRepo = jewelryRepo;
            this.accountRepo = accountRepo;
            this.categoryRepo = categoryRepo;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public IActionResult GetAll()
        {
            return Ok(jewelryRepo.GetSilvers());    
        }

        [EnableQuery]
        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult Add([FromBody] SilverJewelry silverJewelry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(jewelryRepo.addJewelry(silverJewelry));
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        public IActionResult Update([FromBody] SilverJewelry silverJewelry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(jewelryRepo.updateJewelry(silverJewelry));
        }

        [HttpDelete]
        [Authorize(Roles = "1")]
        public IActionResult Delete(string id)
        {

            bool isDeleted = jewelryRepo.deleteJewelry(id);
            if (isDeleted)
            {
                return Ok(new { message = "Item deleted successfully." });
            }
            else
            {
                return BadRequest(new { message = "Error deleting item or item not found." });
            }
        }

    }
}

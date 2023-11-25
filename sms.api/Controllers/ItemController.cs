using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly ItemLogic itemLogic;

        public ItemController(ApplicationDbContext applicationDbContext)
        {
            itemLogic = new ItemLogic(applicationDbContext);
            
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ItemViewModel>> GetAllItems()
        {
            return Ok(itemLogic.GetAllItems());
        }
        [HttpGet("{id:int}", Name = "GetItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ItemViewModel> GetItem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = itemLogic.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ItemViewModel> CreateEnquiry([FromBody] ItemViewModel model)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest(model);
            }
            if (model.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            if (itemLogic.GetAllItems().FirstOrDefault(u => u.Id == model.Id) != null)
            {
                ModelState.AddModelError("", "Item already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = itemLogic.AddItems(model);
            }
            return CreatedAtRoute("GetItem", new { id = model.Id }, model);
        }

        // The following loop sums the squares of the first group of boxed
        // integers in mixedList. The list elements are objects, and cannot
        // be multiplied or added to the sum until they are unboxed. The
        // unboxing must be done explicitly.
        [HttpPut("{id:int}", Name = "UpdateItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateItem(int id, [FromBody] ItemViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var item = itemLogic.UpdateItem(model);
            return Ok(item);

        }

        [HttpDelete("{id:int}", Name = "DeleteItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteItem(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (itemLogic.DeleteItem(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

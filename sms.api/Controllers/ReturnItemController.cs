using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnItemController : ControllerBase
    {
        private readonly ReturnItemLogic returnItemLogic;
        public ReturnItemController(ApplicationDbContext applicationDbContext)
        {
            returnItemLogic = new ReturnItemLogic(applicationDbContext);
            
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ReturnItemViewModel>> GetAllReturnItems()
        {
            return Ok(returnItemLogic.GetReturnItems());
        }
        [HttpGet("{id:int}", Name = "GetReturnItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReturnItemViewModel> GetReturnItem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var ReturnItem = returnItemLogic.GetReturnItem(id);
            if (ReturnItem == null)
            {
                return NotFound();
            }
            return Ok(ReturnItem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ReturnItemViewModel> CreateReturnItem([FromBody] ReturnItemViewModel model)
        {
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
            if (returnItemLogic.GetReturnItems().FirstOrDefault(u => u.Id == model.Id) != null)
            {
                ModelState.AddModelError("", "ReturnItem already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = returnItemLogic.AddReturnItem(model);
            }
            return CreatedAtRoute("GetReturnItem", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateReturnItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateReturnItem(int id, [FromBody] ReturnItemViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var ReturnItem = returnItemLogic.UpdateReturnItem(model);
            return Ok(ReturnItem);
        }

        [HttpDelete("{id:int}", Name = "DeleteReturnItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteReturnItem(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (returnItemLogic.DeleteReturnItem(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

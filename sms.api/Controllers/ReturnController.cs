using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : ControllerBase
    {
        private readonly ReturnLogic returnLogic;
        public ReturnController(ApplicationDbContext applicationDbContext)
        {
            returnLogic = new ReturnLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ReturnViewModel>> GetAllReturn()
        {
            return Ok(returnLogic.GetAllRetunDetails());
        }

        [HttpGet("{id:int}", Name = "GetReturn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReturnViewModel> GetReturn(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var ReturnItem = returnLogic.GetReturn(id);
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
        public ActionResult<ReturnViewModel> CreateReturnItem([FromBody] ReturnViewModel model)
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
            if (returnLogic.GetAllRetunDetails().FirstOrDefault(u => u.ReturnNumber == model.ReturnNumber) != null)
            {
                ModelState.AddModelError("", "ReturnItem already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = returnLogic.AddReturns(model);
            }
            return CreatedAtRoute("GetReturn", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateReturns")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateReturns(int id, [FromBody] ReturnViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var ReturnItem = returnLogic.UpdateReturn(model);
            return Ok(ReturnItem);
        }

        [HttpDelete("{id:int}", Name = "DeleteReturn")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteReturn(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (returnLogic.DeleteReturns(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

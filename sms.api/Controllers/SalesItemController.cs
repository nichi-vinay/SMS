using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemController : ControllerBase
    {
        private readonly SalesItemLogic _salesItemLogic;

        public SalesItemController(ApplicationDbContext applicationDbContext)
        {
            _salesItemLogic = new SalesItemLogic(applicationDbContext);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SalesItemViewModel>> GetAllSalesItems()
        {
            return Ok(_salesItemLogic.GetAllSalesItems());
        }

        [HttpGet("{id:int}", Name = "GetSalesItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SalesItemViewModel> GetSalesItem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var salesItem = _salesItemLogic.GetSalesItem(id);
            if (salesItem == null)
            {
                return NotFound();
            }
            return Ok(salesItem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SalesItemViewModel> CreateSalesItem([FromBody] SalesItemViewModel model)
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
            if (_salesItemLogic.GetAllSalesItems().FirstOrDefault(u => u.Id == model.Id) != null)
            {
                ModelState.AddModelError("", "SalesItem already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = _salesItemLogic.AddSalesItem(model);
            }
            return CreatedAtRoute("GetSalesItem", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateSalesItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSalesItem(int id, [FromBody] SalesItemViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var SalesItem = _salesItemLogic.UpdateSalesItem(model);
            return Ok(SalesItem);
        }

        [HttpDelete("{id:int}", Name = "DeleteSalesItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSalesItem(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (_salesItemLogic.DeleteSalesItem(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

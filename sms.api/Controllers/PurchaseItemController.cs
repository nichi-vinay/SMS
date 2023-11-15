using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseItemController : ControllerBase
    {
        private readonly PurchaseItemLogic purchaseItemLogic;

        public PurchaseItemController(ApplicationDbContext applicationDbContext)
        {
            purchaseItemLogic = new PurchaseItemLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PurchaseItemViewModel>>GetAllPurchaseItems()
        {
            return Ok(purchaseItemLogic.GetAllPurchaseItem());
        }

        [HttpGet("{id:int}", Name = "GetPurchaseItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PurchaseViewModel> GetPurchaseItem(int  id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var purchaseItem =  purchaseItemLogic.GetPurchaseItem(id);
            if(purchaseItem == null)
            {
                return NotFound();
            }
            return Ok(purchaseItem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PurchaseItemViewModel> CreatePurchase([FromBody] PurchaseItemViewModel model)
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
            if(purchaseItemLogic.GetAllPurchaseItem().FirstOrDefault(u=>u.Id==model.Id)!=null)
            {
                ModelState.AddModelError("", "purchaseItem already Exists!");
                return BadRequest(ModelState);
            }
            if(model.Id==0)
            {
                int id = purchaseItemLogic.AddPurchaseItem(model);
            }
            return CreatedAtRoute("GetPurchaseItem", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdatePurchaseItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePurchaseItem(int id, [FromBody] PurchaseItemViewModel model) 
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var purchaseItem = purchaseItemLogic.UpdatePurchaseItem(model);
            return Ok(purchaseItem);
        }

        [HttpDelete("{id:int}", Name = "DeletePurchaseItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePurchaseItem(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (purchaseItemLogic.DeletePurchaseItem(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

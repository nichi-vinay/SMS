using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        private readonly PurchaseLogic purchaseLogic;

        public PurchaseController(ApplicationDbContext applicationDbContext)
        {
            purchaseLogic = new PurchaseLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PurchaseViewModel>>GetAllPurchase()
        {
            return Ok(purchaseLogic.GetAllPurchases());
        }

        [HttpGet("{id:int}", Name = "GetPurchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PurchaseViewModel>GetPurchase(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var purchase = purchaseLogic.GetPurchase(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PurchaseViewModel> CreatePurchase([FromBody] PurchaseViewModel model)
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
            if (purchaseLogic.GetAllPurchases().FirstOrDefault(u => u.InvoiceNumber == model.InvoiceNumber) != null)
            {
                ModelState.AddModelError("", "Invoice already Exists!");
                return BadRequest(ModelState);
            }
            if(model.Id==0) 
            {
                int id = purchaseLogic.AddPurchase(model);
            }
            return CreatedAtRoute("GetPurchase", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdatePurchase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePurchase(int id, [FromBody] PurchaseViewModel model)
        {
            if(model.Id==null ||id!=model.Id)
            {
                return BadRequest(ModelState);
            }
            var purchase = purchaseLogic.UpdatePurchase(model);
            return Ok(purchase);

        }
        [HttpDelete("{id:int}", Name = "DeletePurchase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePurchase(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (purchaseLogic.DeletePurchase(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}

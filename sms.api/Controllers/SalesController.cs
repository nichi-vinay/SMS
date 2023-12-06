using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesLogic salesLogic;

        public SalesController(ApplicationDbContext applicationDbContext)
        {
            salesLogic = new SalesLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SalesViewModel>> Getallsales()
        {
            return Ok(salesLogic.GetAllSales());
        }

        [HttpGet("{id:int}", Name = "GetSales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SalesViewModel> GetSales(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var salesItem = salesLogic.GetSales(id);
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
        public ActionResult<SalesViewModel> CreateSalesItem([FromBody] SalesViewModel model)
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
            if (salesLogic.GetAllSales().FirstOrDefault(u => u.InvoiceNumber == model.InvoiceNumber) != null)
            {
                ModelState.AddModelError("", "SalesItem already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = salesLogic.AddSales(model,model.SalesItems);
            }
            return CreatedAtRoute("GetSales", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateSales")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSales(int id, [FromBody] SalesViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var Sales = salesLogic.UpdateSales(model,model.SalesItems);
            return Ok(Sales);
        }

        [HttpDelete("{id:int}", Name = "DeleteSales")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSales(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (salesLogic.DeleteSales(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

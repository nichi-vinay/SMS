using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxTypeController : ControllerBase
    {
        private readonly TaxTypeLogic _taxTypeLogic;

        public TaxTypeController(ApplicationDbContext applicationDbContext)
        {
            _taxTypeLogic = new TaxTypeLogic(applicationDbContext);
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TaxTypeViewModel>> GetAllTaxType()
        {
            return Ok(_taxTypeLogic.GetAllTaxType());
        }

        [HttpGet("{id:int}", Name = "GetTaxType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TaxTypeViewModel> GetTaxType(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var taxType = _taxTypeLogic.GetTaxType(id);
            if (taxType == null)
            {
                return NotFound();
            }
            return Ok(taxType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TaxTypeViewModel> CreateTaxType([FromBody] TaxTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest(model);
            }
            if (model.TAXTypeId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            if (_taxTypeLogic.GetAllTaxType().FirstOrDefault(u => u.TAXTypeId == model.TAXTypeId) != null)
            {
                ModelState.AddModelError("", "Taxtype already Exists!");
                return BadRequest(ModelState);
            }
            if (model.TAXTypeId == 0)
            {
                int id = _taxTypeLogic.AddTaxType(model);
            }
            return CreatedAtRoute("GetTaxType", new { id = model.TAXTypeId }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateTaxType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTaxType(int id, [FromBody] TaxTypeViewModel model)
        {
            if (model.TAXTypeId == null || id != model.TAXTypeId)
            {
                return BadRequest(ModelState);
            }
            var taxtype = _taxTypeLogic.UpdateTaxType(model);
            return Ok(taxtype);
        }

        [HttpDelete("{id:int}", Name = "DeleteTaxTypes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTaxTypes(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (_taxTypeLogic.DeleteTaxType(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

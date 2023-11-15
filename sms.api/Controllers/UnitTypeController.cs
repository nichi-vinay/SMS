using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTypeController : ControllerBase
    {
        private readonly UnitTypeLogic _unitTypeLogic;

        public UnitTypeController(ApplicationDbContext applicationDbContext)
        {
            _unitTypeLogic = new UnitTypeLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UnitTypeViewModel>> GetAllUnitType()
        {
            return Ok(_unitTypeLogic.GetUnitTypes());
        }


        [HttpGet("{id:int}", Name = "GetUnitType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UnitTypeViewModel> GetUnitType(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var taxType = _unitTypeLogic.GetUnitType(id);
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
        public ActionResult<UnitTypeViewModel> CreateTaxType([FromBody] UnitTypeViewModel model)
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
            if (_unitTypeLogic.GetUnitTypes().FirstOrDefault(u => u.Id == model.Id) != null)
            {
                ModelState.AddModelError("", "UnitType already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = _unitTypeLogic.AddUnitType(model);
            }
            return CreatedAtRoute("GetUnitType", new { id = model.Id }, model);
        }
        [HttpPut("{id:int}", Name = "UpdateUnitType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUnitType(int id, [FromBody] UnitTypeViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var Unittype = _unitTypeLogic.UpdateUnitType(model);
            return Ok(Unittype);
        }
        [HttpDelete("{id:int}", Name = "DeleteUnitTypes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUnitTypes(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (_unitTypeLogic.DeleteUnitType(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

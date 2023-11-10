using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryTypeController : ControllerBase
    {
        private readonly EnquiryTypeLogic enquiryTypeLogic;
        public EnquiryTypeController(ApplicationDbContext applicationDbContext)
        {
            enquiryTypeLogic = new EnquiryTypeLogic(applicationDbContext);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EnquiryTypeViewModel>>GetAllEnquirys()
        {
            return Ok(enquiryTypeLogic.GetAllEnquiry());
        }

        [HttpGet("{id:int}", Name = "GetEnquiry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EnquiryTypeViewModel> GetEnquiry(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var enquiry = enquiryTypeLogic.GetEnquiry(id);
            if (enquiry == null)
            {
                return NotFound();
            }
            return Ok(enquiry);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EnquiryTypeViewModel> CreateEnquiry([FromBody] EnquiryTypeViewModel model)
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
            if (enquiryTypeLogic.GetAllEnquiry().FirstOrDefault(u => u.EnquiryTypeName == model.EnquiryTypeName) != null)
            {
                ModelState.AddModelError("", "EnquiryType already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = enquiryTypeLogic.AddEnquirytype(model);
            }
            return CreatedAtRoute("GetEnquiry", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateEnquiry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateEnquiry(int id, [FromBody] EnquiryTypeViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var Enquiry = enquiryTypeLogic.UpdateEnquiryType(model);
            return Ok(Enquiry);

        }

        [HttpDelete("{id:int}", Name = "DeleteEnquiry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteEnquiry(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (enquiryTypeLogic.DeleteEnquirytype(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}

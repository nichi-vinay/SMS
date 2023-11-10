using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.data.Models;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly VendorLogic VendorLogic;

        public VendorsController(ApplicationDbContext applicationDbContext)
        {
            VendorLogic = new VendorLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VendorViewModel>> GetAllVendor()
        {
            return Ok(VendorLogic.GetAllVendors());
        }

        [HttpGet("{id:int}", Name = "GetVendor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VendorViewModel>GetVendor(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var vendor = VendorLogic.GetVendor(id);
            if(vendor == null)
            {
                return NotFound();
            }
            return Ok(vendor);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VendorViewModel> CreateVendor([FromBody] VendorViewModel vendor)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (vendor == null)
            {
                return BadRequest(vendor);
            }
            if (vendor.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if(VendorLogic.GetAllVendors().FirstOrDefault(u=>u.Mobile==vendor.Mobile)!=null)
            {
                ModelState.AddModelError("", "Vendor already Exists!");
                return BadRequest(ModelState);
            }
            if(vendor.Id==0)
            {
                int id = VendorLogic.AddVendor(vendor);
            }
            return CreatedAtRoute("GetVendor", new { id = vendor.Id }, vendor);
        }

        [HttpDelete("{id:int}", Name = "DeleteVendor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVendor(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = (VendorLogic.DeleteVendor(id)==true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id:int}", Name = "UpdateVendor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVendor(int id, [FromBody]VendorViewModel vendor)
        {
            if(vendor.Id==null||id!=vendor.Id)
            {
                return BadRequest();
            }
            var vendors = VendorLogic.UpdateVendor(vendor);
            return Ok(vendors);
        }
    }
}

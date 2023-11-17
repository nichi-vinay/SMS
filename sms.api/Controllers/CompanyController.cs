using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyMasterLogic companyMasterLogic;

        public CompanyController(ApplicationDbContext applicationDbContext)
        {
            companyMasterLogic = new CompanyMasterLogic(applicationDbContext); 
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CompanyMasterViewModel>> GetAllCompany()
        {
            return Ok(companyMasterLogic.GetAllComapnyDetails());
        }

        [HttpGet("{id:int}", Name = "GetCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CompanyMasterViewModel> GetCompany(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var comapny = companyMasterLogic.GetCompany(id);
            if (comapny == null)
            {
                return NotFound();
            }
            return Ok(comapny);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CompanyMasterViewModel> CreateCompany([FromBody] CompanyMasterViewModel model)
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
            if (companyMasterLogic.GetAllComapnyDetails().FirstOrDefault(u => u.Logo == model.Logo) != null)
            {
                ModelState.AddModelError("", "Comapny already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = companyMasterLogic.AddCompany(model);
            }
            return CreatedAtRoute("GetCompany", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCompany(int id, [FromBody] CompanyMasterViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var company = companyMasterLogic.UpdateCompany(model);
            return Ok(company);

        }

        [HttpDelete("{id:int}", Name = "Deletecompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Deletecompany(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (companyMasterLogic.DeleteCompany(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

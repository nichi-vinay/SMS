using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly DashboardLogic dashboardLogic;

        public DashBoardController(ApplicationDbContext applicationDbContext)
        {
            dashboardLogic = new DashboardLogic(applicationDbContext);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DashboardViewModel>> GetAlldashboard()
        {
            return Ok(dashboardLogic.GetAllDashboard());
        }
        [HttpGet("{id:int}", Name = "GetDashBoard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DashboardViewModel> GetDashBoard(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var dashboard= dashboardLogic.GetDashboard(id);
            if (dashboard == null)
            {
                return NotFound();
            }
            return Ok(dashboard);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DashboardViewModel> Createdashboard([FromBody] DashboardViewModel model)
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
            if (dashboardLogic.GetAllDashboard().FirstOrDefault(u => u.Id == model.Id) != null)
            {
                ModelState.AddModelError("", "dashboard details already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = dashboardLogic.AddDashboard(model);
            }
            return CreatedAtRoute("GetDashBoard", new { id = model.Id }, model);
        }
        [HttpPut("{id:int}", Name = "Updatedashboard")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Updatedashboard(int id, [FromBody] DashboardViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var dashboard = dashboardLogic.UpdateDashboard(model);
            return Ok(dashboard);
        }
        [HttpDelete("{id:int}", Name = "Deletedashboard")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Deletedashboard(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (dashboardLogic.DeleteDashBoard(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

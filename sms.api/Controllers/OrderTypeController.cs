using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypeController : ControllerBase
    {
        private readonly OrderTypeLogic orderTypeLogic;

        public OrderTypeController(ApplicationDbContext applicationDbContext)
        {
            orderTypeLogic = new OrderTypeLogic(applicationDbContext);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderTypeViewModel>> GetAllOrderType()
        {
            return Ok(orderTypeLogic.GetOrderTypes());
        }
        [HttpGet("{id:int}", Name = "GetOrderType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OrderItemViewModel> GetOrderType(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = orderTypeLogic.GetOrderType(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderTypeViewModel> CreateEnquiry([FromBody] OrderTypeViewModel model)
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
            if (orderTypeLogic.GetOrderTypes().FirstOrDefault(u => u.Id == model.Id) != null)
            {
                ModelState.AddModelError("", "OrderedTypes already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = orderTypeLogic.AddOrderType(model);
            }
            return CreatedAtRoute("GetOrderType", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateOrderType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateOrderType(int id, [FromBody] OrderTypeViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var item = orderTypeLogic.UpdateOrdertype(model);
            return Ok(item);

        }
        [HttpDelete("{id:int}", Name = "DeleteOrderTypes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteOrderTypes(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (orderTypeLogic.DeleteOrderType(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}

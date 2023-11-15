using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemLogic orderItemLogic;

        public OrderItemController(ApplicationDbContext applicationDbContext)
        {
            orderItemLogic = new OrderItemLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderItemViewModel>> GetAllOrderItems()
        {
            return Ok(orderItemLogic.GetOrderItems());
        }
        [HttpGet("{id:int}", Name = "GetOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OrderItemViewModel> GetOrderItem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = orderItemLogic.GetOrderItem(id);
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
        public ActionResult<OrderItemViewModel> CreateEnquiry([FromBody] OrderItemViewModel model)
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
            if (orderItemLogic.GetOrderItems().FirstOrDefault(u => u.Id == model.Id) != null)
            {
                ModelState.AddModelError("", "OrderedItem already Exists!");
                return BadRequest(ModelState);
            }
            if (model.Id == 0)
            {
                int id = orderItemLogic.AddOrderItem(model);
            }
            return CreatedAtRoute("GetOrderItem", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateOrderItems")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateOrderItems(int id, [FromBody] OrderItemViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var item = orderItemLogic.UpdateOrderItem(model);
            return Ok(item);

        }

        [HttpDelete("{id:int}", Name = "DeleteOrderItems")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteOrderItems(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (orderItemLogic.DeleteOrderItem(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

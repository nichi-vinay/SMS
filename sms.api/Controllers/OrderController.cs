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
    public class OrdersController : ControllerBase
    {
        private readonly OrderLogic OrderLogic;

        public OrdersController(ApplicationDbContext applicationDbContext)
        {
            OrderLogic = new OrderLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderViewModel>> GetAllOrders()
        {
            return Ok(OrderLogic.GetAllOrders());
        }

        [HttpGet("{id:int}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OrderViewModel> GetOrder(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var order = OrderLogic.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderViewModel> CreateOrder([FromBody] OrderViewModel order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (order == null)
            {
                return BadRequest(order);
            }
            if (order.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            int id = OrderLogic.AddOrder(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }

        [HttpDelete("{id:int}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteOrder(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = (OrderLogic.DeleteOrder(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id:int}", Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateOrder(int id, [FromBody] OrderViewModel order)
        {
            if (order.Id == null || id != order.Id)
            {
                return BadRequest();
            }
            var orders = OrderLogic.UpdateOrder(order);
            return Ok(orders);
        }
    }
}

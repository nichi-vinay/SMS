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
    public class CustomersController : ControllerBase
    {
        private readonly CustomerLogic CustomerLogic;

        public CustomersController(ApplicationDbContext applicationDbContext)
        {
            CustomerLogic = new CustomerLogic(applicationDbContext);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CustomerViewModel>> GetAllCustomers()
        {
            return Ok(CustomerLogic.GetAllCustomers());
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CustomerViewModel> GetCustomer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var customer = CustomerLogic.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CustomerViewModel> CreateCustomer([FromBody] CustomerViewModel customer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (customer == null)
            {
                return BadRequest(customer);
            }
            if (customer.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (CustomerLogic.GetAllCustomers().FirstOrDefault(u => u.Mobile == customer.Mobile) != null)
            {
                ModelState.AddModelError("", "Customer already Exists!");
                return BadRequest(ModelState);
            }
            if (customer.Id == 0)
            {
                int id = CustomerLogic.AddCustomer(customer);
            }
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpDelete("{id:int}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = (CustomerLogic.DeleteCustomer(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id:int}", Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerViewModel customer)
        {
            if (customer.Id == null || id != customer.Id)
            {
                return BadRequest();
            }
            var customers = CustomerLogic.UpdateCustomer(customer);
            return Ok(customers);
        }
    }
}

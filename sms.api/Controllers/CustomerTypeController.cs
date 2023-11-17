using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly CustomerTypeLogic customerTypeLogic;

        public CustomerTypeController(ApplicationDbContext applicationDbContext)
        {
            customerTypeLogic = new CustomerTypeLogic(applicationDbContext);
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CustomerTypeViewModel>> GetAllCustomerType()
        {
            return Ok(customerTypeLogic.GetAllCustomerTypeList());
        }

        [HttpGet("{id:int}", Name = "GetCustomerType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CustomerTypeViewModel> GetCustomerType(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var customertype = customerTypeLogic.GetCustomerType(id);
            if (customertype == null)
            {
                return NotFound();
            }
            return Ok(customertype);
        }
    }
}

//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using sms.biz.Logic;
//using sms.data;
//using sms.viewmodels;

//namespace sms.api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SalesTransactionController : ControllerBase
//    {
//        private readonly SalesTransactionLogic _salesTransactionLogic;

//        public SalesTransactionController(ApplicationDbContext applicationDbContext)
//        {
//            _salesTransactionLogic = new SalesTransactionLogic(applicationDbContext);
//        }

//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public ActionResult<IEnumerable<SalesTransactionViewModel>>GetSalesTransactionsDetails()
//        {
//            return Ok(_salesTransactionLogic.GetAllSalesTransaction());
//        }
//        [HttpGet("{id:int}",Name ="GetSalesTransaction")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public ActionResult<SalesTransactionViewModel> GetSalesTransactionDetails(int id)
//        {
//            if (id == 0)
//            {
//                return BadRequest();
//            }
//            var salesTransaction = _salesTransactionLogic.GetSalesTransaction(id);
//            if (salesTransaction == null)
//            {
//                return NotFound();
//            }
//            return Ok(salesTransaction);
//        }
//    }
//}

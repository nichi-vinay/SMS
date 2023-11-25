using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.biz.Logic;
using sms.data;
using sms.viewmodels;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

using System.Drawing;
using ZXing.Rendering;
using sms.data.Models;
using IronBarCode;
using System.IO;
using Newtonsoft.Json;

namespace sms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        private readonly PurchaseLogic purchaseLogic;
        private IWebHostEnvironment _webHostEnvironment;

        public PurchaseController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            purchaseLogic = new PurchaseLogic(applicationDbContext);
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PurchaseViewModel>> GetAllPurchase()
        {
            return Ok(purchaseLogic.GetAllPurchases());
        }

        [HttpGet("{id:int}", Name = "GetPurchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PurchaseViewModel> GetPurchase(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var purchase = purchaseLogic.GetPurchase(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PurchaseViewModel> CreatePurchase([FromBody] PurchaseViewModel model)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
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
          
            if (model.Id == 0 && model.PurchaseItems != null && model.PurchaseItems.Any())
            {
                int id = purchaseLogic.AddPurchase(model, model.PurchaseItems);
            }
            return CreatedAtRoute("GetPurchase", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}", Name = "UpdatePurchase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePurchase(int id, [FromBody] PurchaseViewModel model)
        {
            if (model.Id == null || id != model.Id)
            {
                return BadRequest(ModelState);
            }
            var purchase = purchaseLogic.UpdatePurchase(model);
            return Ok(purchase);

        }
        [HttpDelete("{id:int}", Name = "DeletePurchase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePurchase(int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }
            var item = (purchaseLogic.DeletePurchase(id) == true) ? "success" : "fail";
            if (item == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPost("barcode")]
        public IActionResult Barcode()
        {
            // Generate a random barcode (you can customize the length and characters as needed)
            string randomBarcode = GenerateRandomBarcode(12); // Change the length as needed

            // Create the barcode instance and generate the barcode image
            GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(randomBarcode, BarcodeWriterEncoding.Code128);
            barcode.ResizeTo(500, 150);
            barcode.ChangeBarCodeColor(System.Drawing.Color.Black);

            // Set the path for saving the barcode image
            string localPath = @"D:\VisualStudio\Projects\NichiProjects\SMSMain\sms.api\BarcodeImage\BarCodeFile\";
            string path = Path.Combine(localPath, "BarCodeFile");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = Path.Combine(path, $"_aa{randomBarcode}.png");
            barcode.SaveAsPng(filepath);

            // Construct the image URL
            string filename = Path.GetFileName(filepath);
            string imageURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/BarCodeFile" + filename;

            return Ok(new { imageURL });
        }

        // Helper method to generate a random barcode
        private string GenerateRandomBarcode(int length)
        {
            const string chars = "0123456789"; // You can include other characters if needed
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }

}


using sms.biz.Map;
using sms.data;
using sms.data.Models;
using sms.viewmodels;


namespace sms.biz.Logic
{
    public class PurchaseLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PurchaseLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public List<PurchaseViewModel> GetAllPurchases()
        {
            return PurchaseMap.MapGetPurchase(_applicationDbContext.PurchaseMaster.Where(x => x.IsActive == true).ToList(), _applicationDbContext);
        }


        public PurchaseViewModel GetPurchase(int id)
        {
            return PurchaseMap.GetPurchaseDetails(_applicationDbContext.PurchaseMaster.FirstOrDefault(x => x.Id == id), _applicationDbContext);
        }


        public int AddPurchase(PurchaseViewModel purchaseViewModel, List<PurchaseItemViewModel> purchaseItems)
        {
            var purchaseMaster = PurchaseMap.MapCreatePurchase(purchaseViewModel, purchaseItems, _applicationDbContext);


            _applicationDbContext.SaveChanges();

            return purchaseMaster.Id;
        }


        public bool DeletePurchase(int id)
        {
                var purchase = _applicationDbContext.PurchaseMaster.FirstOrDefault( x => x.Id==id);
                if (purchase != null)
                {
                    purchase.IsActive = false;
                    _applicationDbContext.SaveChanges();
                    return true;
                }
                return false;
        }

        public bool UpdatePurchase(PurchaseViewModel purchaseViewModel)
        {
            var purchasemaster = _applicationDbContext.PurchaseMaster.FirstOrDefault(x => x.Id == purchaseViewModel.Id);
            if (purchasemaster != null)
            {
                purchasemaster.InvoiceDate = purchaseViewModel.InvoiceDate;
                purchasemaster.InvoiceNumber = purchaseViewModel.InvoiceNumber;
                purchasemaster.VendorId = purchaseViewModel.VendorId;
                purchasemaster.IsSubmitted = purchaseViewModel.IsSubmitted;
                purchasemaster.ShipmentDetails = purchaseViewModel.ShipmentDetails;
                purchasemaster.IsActive = true;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

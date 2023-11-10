using sms.biz.Map;
using sms.data;
using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Logic
{
    public class PurchaseLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PurchaseLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<PurchaseViewModel>GetAllPurchases()
        {
            return PurchaseMap.MapGetPurchase(_applicationDbContext.PurchaseMaster.Where(x=>x.IsActive==true).ToList());
        }

        public PurchaseViewModel GetPurchase(int id)
        {
            return PurchaseMap.GetPurchaseDetails(_applicationDbContext.PurchaseMaster.FirstOrDefault(x=>x.Id==id));

           
        }
        
        public int AddPurchase(PurchaseViewModel purchaseViewModel)
        {
            PurchaseMaster purchaseMaster = PurchaseMap.MapCreatePurchase(purchaseViewModel);


            var entityEntry = _applicationDbContext.PurchaseMaster.Add(purchaseMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
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
            using (var context = _applicationDbContext)
            {
                var purchasemaster = context.PurchaseMaster.FirstOrDefault(x=>x.Id == purchaseViewModel.Id);
                if (purchasemaster != null)
                {
                    purchasemaster.InvoiceDate = purchaseViewModel.InvoiceDate;
                    purchasemaster.InvoiceNumber = purchaseViewModel.InvoiceNumber;
                    purchasemaster.VendorId = purchaseViewModel.VendorId;
                    purchasemaster.IsSubmitted = purchaseViewModel.IsSubmitted;
                    purchasemaster.ShipmentDetails = purchaseViewModel.ShipmentDetails;
                    purchasemaster.IsActive = true;

                    context.SaveChanges();
                    return true;
                    
                }
                return false;
            }
        }
    }
}

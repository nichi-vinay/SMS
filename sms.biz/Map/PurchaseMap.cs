using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class PurchaseMap
    {
        public static PurchaseViewModel GetPurchaseDetails(this PurchaseMaster purchaseMaster)
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel()
            {
                Id = purchaseMaster.Id,
                InvoiceNumber = purchaseMaster.InvoiceNumber,
                VendorId = purchaseMaster.VendorId,
                InvoiceDate = purchaseMaster.InvoiceDate,
                IsActive = purchaseMaster.IsActive,
                IsSubmitted = purchaseMaster.IsSubmitted,
                ShipmentDetails = purchaseMaster.ShipmentDetails,
            };
            return purchaseViewModel;
        }

        public static List<PurchaseViewModel>MapGetPurchase(this List<PurchaseMaster> purchaseMasterList)
        {
            return purchaseMasterList.Select(x=>x.GetPurchaseDetails()).ToList();
        }

        public static PurchaseMaster MapCreatePurchase(this PurchaseViewModel purchaseViewModel)
        {
            return new PurchaseMaster
            {
                Id = purchaseViewModel.Id,
                InvoiceNumber = purchaseViewModel.InvoiceNumber,
                VendorId = purchaseViewModel.VendorId,
                InvoiceDate = purchaseViewModel.InvoiceDate,
                IsActive = purchaseViewModel.IsActive,
                IsSubmitted = purchaseViewModel.IsSubmitted,
                ShipmentDetails = purchaseViewModel.ShipmentDetails,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

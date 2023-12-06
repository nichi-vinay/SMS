using Microsoft.EntityFrameworkCore;
using sms.data;
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
       
        public static PurchaseViewModel GetPurchaseDetails(this PurchaseMaster purchaseMaster, ApplicationDbContext context)
        {
            var query = from pm in context.PurchaseMaster
                        join pi in context.PurchaseItemMaster on pm.Id equals pi.PurchasemasterId
                        where pm.Id == purchaseMaster.Id
                        select new PurchaseViewModel
                        {
                            Id = pm.Id,
                            InvoiceNumber = pm.InvoiceNumber,
                            VendorId = pm.VendorId,
                            InvoiceDate = pm.InvoiceDate,
                            IsActive = pm.IsActive,
                            IsSubmitted = pm.IsSubmitted,
                            ShipmentDetails = pm.ShipmentDetails,
                            SupplierName = pm.VendorMaster.Name,
                            TotalAmount = pm.TotalAmount,
                            TotalDiscount = pm.TotalDiscount
                        };

            return query.FirstOrDefault();
        }



        public static List<PurchaseViewModel> MapGetPurchase(this List<PurchaseMaster> purchaseMasterList, ApplicationDbContext context)
        {
            return purchaseMasterList.Select(x => x.GetPurchaseDetails(context)).ToList();
        }
        public static PurchaseMaster MapCreatePurchase(this PurchaseViewModel purchaseViewModel, List<PurchaseItemViewModel> purchaseItems, ApplicationDbContext dbContext)
        {
            var purchaseMaster = new PurchaseMaster
            {
                InvoiceNumber = purchaseViewModel.InvoiceNumber,
                VendorId = purchaseViewModel.VendorId,
                InvoiceDate = purchaseViewModel.InvoiceDate,
                IsActive = true,
                IsSubmitted = purchaseViewModel.IsSubmitted,
                ShipmentDetails = purchaseViewModel.ShipmentDetails,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
                Cash = purchaseViewModel.Cash,
                Cards = purchaseViewModel.Cards,
                Cheque = purchaseViewModel.Cheque,
                Online = purchaseViewModel.Online,
                TotalAmount = purchaseViewModel.TotalAmount,
                TotalDiscount = purchaseViewModel.TotalDiscount,
                TotalPaid = purchaseViewModel.TotalPaid,
                TotalTax = purchaseViewModel.TotalTax
                
            };

            var savedPurchaseMaster = dbContext.PurchaseMaster.Add(purchaseMaster);
            dbContext.SaveChanges();
            int newPurchaseMasterId = savedPurchaseMaster.Entity.Id;

            var purchaseItemMasters = purchaseItems.Select(item => new PurchaseItemMaster
            {
                PurchasemasterId = newPurchaseMasterId, // Use the obtained ID
                ItemID = item.ItemID,
                Quantity = item.Quantity,
                Mrp = item.Mrp,
                DiscountPercentage = item.DiscountPercentage,
                Totalprice = item.Totalprice,
                Barcode = item.Barcode,
                IsActive = true,
                CreatedBy=1,
                CreatedDate = System.DateTime.Now,
            }).ToList();

            // Assign the mapped purchaseItemMasters to purchaseMaster
            purchaseMaster.purchaseItemMasters = purchaseItemMasters;

            return purchaseMaster;
        }
    }
}

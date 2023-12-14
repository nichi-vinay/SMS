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
            var query = context.PurchaseMaster
                        .Include(pm => pm.purchaseItemMasters)
                        .Include(ptm => ptm.PurchaseTransactionsMaster)
                        .Where(nm => nm.Id == purchaseMaster.Id);
            var result = query.Select(mm => new PurchaseViewModel
            {
                Id=purchaseMaster.Id,
                VendorId = mm.VendorId,
                SupplierName = mm.VendorMaster.Name,
                InvoiceDate = mm.InvoiceDate,
                InvoiceNumber = mm.InvoiceNumber,
                ShipmentDetails = mm.ShipmentDetails,
                IsSubmitted = mm.IsSubmitted,
                IsActive = mm.IsActive,
                TotalAmount = mm.TotalAmount,
                TotalDiscount = mm.TotalDiscount,
                TotalTax = mm.TotalTax,
                Cards = mm.PurchaseTransactionsMaster.Cards,
                Cash = mm.PurchaseTransactionsMaster.Cash,
                Cheque = mm.PurchaseTransactionsMaster.Cheque,
                Online  = mm.PurchaseTransactionsMaster.Online,
                TotalPaid =mm.PurchaseTransactionsMaster.TotalPaid,
                PurchaseItems =mm.purchaseItemMasters.Select(pi=> new PurchaseItemViewModel
                { 
                    Id=pi.Id,
                    ItemName = pi.ItemMaster.ItemName,
                    ItemNumber = pi.ItemMaster.ItemNumber,
                    PurchasemasterId = pi.PurchasemasterId,
                    ItemID = pi.ItemID,
                    Quantity = pi.Quantity,
                    Mrp = pi.Mrp,
                    DiscountPercentage=pi.DiscountPercentage,
                    Totalprice = pi.Totalprice,
                    IsActive=pi.IsActive,
                    Barcode = pi.Barcode,



                }).ToList(),


            }).FirstOrDefault();



            return result ?? new PurchaseViewModel();
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
              
                TotalAmount = purchaseViewModel.TotalAmount,
                TotalDiscount = purchaseViewModel.TotalDiscount,
                
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
            var purchaseTransaction = new PurchaseTransactionsMaster
            {
                Cards = purchaseViewModel.Cards,
                Cash = purchaseViewModel.Cash,
                Cheque = purchaseViewModel.Cheque,
                Online = purchaseViewModel.Online,
                TotalPaid = purchaseViewModel.TotalPaid,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
            };
            purchaseMaster.PurchaseTransactionsMaster = purchaseTransaction;

            return purchaseMaster;
        }
    }
}

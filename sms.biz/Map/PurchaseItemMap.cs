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
    public static class PurchaseItemMap
    {
        public static PurchaseItemViewModel GetallPurchaseItemDetails(this PurchaseItemMaster purchaseItemMaster, ApplicationDbContext context)
        {
            var query = from pm in context.ItemMaster
                        join it in context.PurchaseItemMaster on pm.Id equals it.ItemID
                        join qm in context.PurchaseMaster on it.PurchasemasterId equals qm.Id into purchaseMasterGroup
                        from qm in purchaseMasterGroup.DefaultIfEmpty() // Left Join

                        where it.Id == purchaseItemMaster.Id
                        select new PurchaseItemViewModel
                        {
                            Id = it.Id,
                            ItemID = it.ItemID,
                            PurchasemasterId = it.PurchasemasterId,
                            Quantity = it.Quantity,
                            Mrp = it.Mrp,
                            DiscountPercentage = it.DiscountPercentage,
                            Totalprice = it.Totalprice,
                            //IsSubmitted = it.IsSubmitted,
                            IsActive = it.IsActive,
                            Barcode = it.Barcode,
                            ItemName = pm.ItemName,
                            ItemNumber = pm.ItemNumber
                           
                        };

            return query.FirstOrDefault();

        }

        public static List<PurchaseItemViewModel>MapPurchaseItem(this List<PurchaseItemMaster> purchaseItems, ApplicationDbContext context)
        {
            return purchaseItems.Select(x =>x.GetallPurchaseItemDetails(context)).ToList();
        }
       

        public static PurchaseItemMaster MapCreatePurchase(this PurchaseItemViewModel purchaseItemViewModel)
        {
            return new PurchaseItemMaster
            {
                Id = purchaseItemViewModel.Id,
                ItemID = purchaseItemViewModel.ItemID,
                PurchasemasterId = purchaseItemViewModel.PurchasemasterId,
                Quantity = purchaseItemViewModel.Quantity,
                Mrp = purchaseItemViewModel.Mrp,
                DiscountPercentage = purchaseItemViewModel.DiscountPercentage,
                Totalprice = purchaseItemViewModel.Totalprice,
                //IsSubmitted = purchaseItemViewModel.IsSubmitted,
                IsActive = purchaseItemViewModel.IsActive,
                Barcode = purchaseItemViewModel.Barcode,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
                
            };
        }
    }
}

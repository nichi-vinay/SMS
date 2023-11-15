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
        public static PurchaseItemViewModel GetallPurchaseItemDetails(this PurchaseItemMaster purchaseItemMaster )
        {
            PurchaseItemViewModel viewModel = new PurchaseItemViewModel()
            {
                Id = purchaseItemMaster.Id,
                ItemID = purchaseItemMaster.ItemID,
                PurchasemasterId  = purchaseItemMaster.PurchasemasterId,
                Quantity = purchaseItemMaster.Quantity,
                Mrp = purchaseItemMaster.Mrp,
                DiscountPercentage = purchaseItemMaster.DiscountPercentage,
                Totalprice = purchaseItemMaster.Totalprice,
                IsSubmitted = purchaseItemMaster.IsSubmitted,
                IsActive = purchaseItemMaster.IsActive
                
            };
            return viewModel;
        }

        public static List<PurchaseItemViewModel>MapPurchaseItem(this List<PurchaseItemMaster> purchaseItems)
        {
            return purchaseItems.Select(x =>x.GetallPurchaseItemDetails()).ToList();
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
                IsSubmitted = purchaseItemViewModel.IsSubmitted,
                IsActive = purchaseItemViewModel.IsActive,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
           
            };
        }
    }
}

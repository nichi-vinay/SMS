using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class SalesItemMap
    {
        public static SalesItemViewModel GetAllSalesItem(this SalesItemMaster salesItemMaster)
        {
            SalesItemViewModel viewModel = new SalesItemViewModel
            {
                Id = salesItemMaster.Id,
                SalesmasterId = salesItemMaster.Id,
                ItemID = salesItemMaster.ItemID,
                Quantity = salesItemMaster.Quantity,
                Mrp = salesItemMaster.Mrp,
                DiscountPercentage = salesItemMaster.DiscountPercentage,
                TotalPrice = salesItemMaster.TotalPrice,
                IsActive = salesItemMaster.IsActive,
                CreatedBy = salesItemMaster.CreatedBy,
                CreatedDate = salesItemMaster.CreatedDate
            };
            return viewModel;
        }

        public static List<SalesItemViewModel>MapGetSalesItem(this List<SalesItemMaster> salesList)
        {
            return salesList.Select(x=>x.GetAllSalesItem()).ToList();
        }

        public static SalesItemMaster MapCreateSalesItem(this SalesItemViewModel salesItemMaster)
        {
            return new SalesItemMaster
            {
                Id = salesItemMaster.Id,
                SalesmasterId = salesItemMaster.SalesmasterId,
                ItemID = salesItemMaster.ItemID,
                Quantity = salesItemMaster.Quantity,
                Mrp = salesItemMaster.Mrp,
                DiscountPercentage = salesItemMaster.DiscountPercentage,
                TotalPrice = salesItemMaster.TotalPrice,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
            };
        }

    }
}

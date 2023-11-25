using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class ItemMap
    {
        public static ItemViewModel GetAllItemsDetails(this ItemMaster itemMaster)
        {
            ItemViewModel viewModel = new ItemViewModel()
            {
                Id = itemMaster.Id,
                ItemName = itemMaster.ItemName,
                ItemNumber = itemMaster.ItemNumber,
                UnitTypeMasterId = itemMaster.UnitTypeMasterId,
                IsActive = itemMaster.IsActive,
            };
            return viewModel;
        }

        public static List<ItemViewModel>MapGetItems(this List<ItemMaster> itemMaster)
        {
            return itemMaster.Select(x=>x.GetAllItemsDetails()).ToList();
        }

        public static ItemMaster MapCreateItems(this ItemViewModel itemViewModel)
        {
            return new ItemMaster
            {
                Id=itemViewModel.Id,
                ItemName=itemViewModel.ItemName,
                ItemNumber=itemViewModel.ItemNumber,
                UnitTypeMasterId=itemViewModel.UnitTypeMasterId,
                IsActive = itemViewModel.IsActive,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static  class ReturnItemMap
    {
        public static ReturnItemViewModel GetAllReturnItem(this ReturnItemMaster returnMaster)
        {
            ReturnItemViewModel viewModel = new ReturnItemViewModel
            {
                Id = returnMaster.Id,
                ItemID = returnMaster.ItemID,
                ReturnMasterId = returnMaster.ReturnMasterId,
                Mrp = returnMaster.Mrp,
                Total = returnMaster.Total,
                IsActive = returnMaster.IsActive,
                CreatedBy = returnMaster.CreatedBy,
                CreatedDate = returnMaster.CreatedDate
            };
            return viewModel;
        }

        public static List<ReturnItemViewModel>MapGetReturnItem(this List<ReturnItemMaster> returnMaster)
        {
            return returnMaster.Select(x=>x.GetAllReturnItem()).ToList();
        }

        public static ReturnItemMaster MapCreateReturnItem(this ReturnItemViewModel returnItemViewModel)
        {
            return new ReturnItemMaster
            {
                Id = returnItemViewModel.Id,
                ItemID = returnItemViewModel.ItemID,
                ReturnMasterId = returnItemViewModel.ReturnMasterId,
                Mrp = returnItemViewModel.Mrp,
                Total = returnItemViewModel.Total,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

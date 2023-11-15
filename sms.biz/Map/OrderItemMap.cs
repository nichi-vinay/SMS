using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class OrderItemMap
    {
        public static OrderItemViewModel GetAllOrderItem(this OrderItmeMaster orderItmeMaster)
        {
            OrderItemViewModel orderItemViewModel = new OrderItemViewModel()
            {
                Id = orderItmeMaster.Id,
                OrderMasterId = orderItmeMaster.OrderMasterId,
                ItemID = orderItmeMaster.ItemID,
                IsProcessed = orderItmeMaster.IsProcessed,
                IsActive = orderItmeMaster.IsActive,
                CreatedBy = orderItmeMaster.CreatedBy,
                CreatedDate = orderItmeMaster.CreatedDate
            };
            return orderItemViewModel;
        }

        public static List<OrderItemViewModel>MapGetOrderItem(this List<OrderItmeMaster> orderItmeMasterList)
        {
            return orderItmeMasterList.Select(x=>x.GetAllOrderItem()).ToList();    
        }

        public static OrderItmeMaster MapCreateOrderItem(this OrderItemViewModel orderItemViewModel)
        {
            return new OrderItmeMaster
            {
                Id = orderItemViewModel.Id,
                OrderMasterId = orderItemViewModel.OrderMasterId,
                ItemID = orderItemViewModel.ItemID,
                IsProcessed = orderItemViewModel.IsProcessed,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

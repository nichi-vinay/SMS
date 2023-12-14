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
        public static OrderItemViewModel GetAllOrderItem(this OrderItmeMaster OrderItmeMaster)
        {
            OrderItemViewModel orderItemViewModel = new OrderItemViewModel()
            {
                Id = OrderItmeMaster.Id,
                OrderMasterId = OrderItmeMaster.OrderMasterId,
                ItemID = OrderItmeMaster.ItemID,
              Quantity = OrderItmeMaster.Quantity,
                IsActive = OrderItmeMaster.IsActive,
                CreatedBy = OrderItmeMaster.CreatedBy,
                CreatedDate = OrderItmeMaster.CreatedDate
            };
            return orderItemViewModel;
        }

        public static List<OrderItemViewModel>MapGetOrderItem(this List<OrderItmeMaster> OrderItmeMasterList)
        {
            return OrderItmeMasterList.Select(x=>x.GetAllOrderItem()).ToList();    
        }

        public static OrderItmeMaster MapCreateOrderItem(this OrderItemViewModel orderItemViewModel)
        {
            return new OrderItmeMaster
            {
                Id = orderItemViewModel.Id,
                OrderMasterId = orderItemViewModel.OrderMasterId,
                ItemID = orderItemViewModel.ItemID,
                Quantity = orderItemViewModel.Quantity,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

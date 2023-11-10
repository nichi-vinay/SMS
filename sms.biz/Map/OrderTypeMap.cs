using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class OrderTypeMap
    {
        public static OrderTypeViewModel GetAllOrdertype(this OrderTypeMaster orderTypeMaster)
        {
            OrderTypeViewModel orderTypeViewModel = new OrderTypeViewModel()
            {
                Id = orderTypeMaster.Id,
                OrderTypeName = orderTypeMaster.OrderTypeName,
                IsActive = orderTypeMaster.IsActive,
                CreatedBy = orderTypeMaster.CreatedBy,
                CreatedDate = orderTypeMaster.CreatedDate
            };
            return orderTypeViewModel;
        }

        public static List<OrderTypeViewModel>MapGetOrderType(this List<OrderTypeMaster> orderTypeMasterList)
        {
            return orderTypeMasterList.Select(x=>x.GetAllOrdertype()).ToList();
        }

        public static OrderTypeMaster MapCreateOrderType(this OrderTypeViewModel orderItemViewModel)
        {
            return new OrderTypeMaster
            {
                Id = orderItemViewModel.Id,
                OrderTypeName = orderItemViewModel.OrderTypeName,
                IsActive = orderItemViewModel.IsActive,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

using sms.biz.Map;
using sms.data;
using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Logic
{
    public class OrderTypeLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OrderTypeLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public List<OrderTypeViewModel> GetOrderTypes()
        {
            return OrderTypeMap.MapGetOrderType(_applicationDbContext.OrderTypeMaster.Where(x => x.IsActive == true).ToList());
        }

        public OrderTypeViewModel GetOrderType(int id)
        {
            return OrderTypeMap.GetAllOrdertype(_applicationDbContext.OrderTypeMaster.FirstOrDefault(x => x.Id == id));
        }

        public int AddOrderType(OrderTypeViewModel orderItem)
        {
            OrderTypeMaster orderItmeMaster = OrderTypeMap.MapCreateOrderType(orderItem);

            var EntityEntry = _applicationDbContext.OrderTypeMaster.Add(orderItmeMaster);
            _applicationDbContext.SaveChanges();
            return EntityEntry.Entity.Id;
        }

        public bool DeleteOrderType(int id)
        {
            var Item = _applicationDbContext.OrderTypeMaster.FirstOrDefault(x => x.Id == id);
            if (Item != null)
            {
                Item.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateOrdertype(OrderTypeViewModel item)
        {
            var items = _applicationDbContext.OrderTypeMaster.FirstOrDefault(x => x.Id == item.Id);
            if (items != null)
            {
                items.OrderTypeName = item.OrderTypeName;
                items.IsActive = true;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

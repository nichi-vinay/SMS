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
    public class OrderItemLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderItemLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<OrderItemViewModel> GetOrderItems()
        {
            return OrderItemMap.MapGetOrderItem(_applicationDbContext.OrderItmeMaster.Where(x=>x.IsActive==true).ToList());
        }

        public OrderItemViewModel GetOrderItem(int id)
        {
            return OrderItemMap.GetAllOrderItem(_applicationDbContext.OrderItmeMaster.FirstOrDefault(x=>x.Id==id));
        }

        public int  AddOrderItem(OrderItemViewModel orderItem)
        {
            OrderItmeMaster orderItmeMaster = OrderItemMap.MapCreateOrderItem(orderItem);

            var EntityEntry = _applicationDbContext.OrderItmeMaster.Add(orderItmeMaster);
            _applicationDbContext.SaveChanges();
            return EntityEntry.Entity.Id;
        }

        public bool DeleteOrderItem(int id)
        {
            var Item = _applicationDbContext.OrderItmeMaster.FirstOrDefault(x => x.Id == id);
            if (Item != null)
            {
                Item.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateOrderItem(OrderItemViewModel item)
        {
            var items = _applicationDbContext.OrderItmeMaster.FirstOrDefault(x => x.Id == item.Id);
            if (items != null)
            {
                items.OrderMasterId = item.OrderMasterId;
                items.ItemID = item.ItemID;
                items.IsProcessed = item.IsProcessed;
                items.IsActive = true;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

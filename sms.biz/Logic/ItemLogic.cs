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
    public class ItemLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ItemLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<ItemViewModel>GetAllItems()
        {
            return ItemMap.MapGetItems(_applicationDbContext.ItemMaster.Where(x=>x.IsActive==true).ToList());
        }

        public ItemViewModel GetItem(int id)
        {
            return ItemMap.GetAllItemsDetails(_applicationDbContext.ItemMaster.FirstOrDefault(x=>x.Id==id));
        }

        public int AddItems(ItemViewModel item)
        {
            ItemMaster itemMaster = ItemMap.MapCreateItems(item);
            var EntityEntry = _applicationDbContext.ItemMaster.Add(itemMaster);
            _applicationDbContext.SaveChanges();
            return EntityEntry.Entity.Id;
        }

        public bool DeleteItem(int id)
        {
            var Item = _applicationDbContext.ItemMaster.FirstOrDefault(x=>x.Id == id);
            if (Item != null)
            {
                Item.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateItem(ItemViewModel item)
        {
            var items = _applicationDbContext.ItemMaster.FirstOrDefault(x=>x.Id==item.Id);
            if (items != null)
            {
                items.ItemName = item.ItemName;
                items.ItemNumber = item.ItemNumber;
                items.UnitTypeMasterId = item.UnitTypeMasterId;
                items.IsActive = true;
                _applicationDbContext.SaveChanges();
                return true;

            }
            return false;
        }
    }
}

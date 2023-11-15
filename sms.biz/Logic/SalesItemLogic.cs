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
    public class SalesItemLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SalesItemLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<SalesItemViewModel> GetAllSalesItems()
        {
            return SalesItemMap.MapGetSalesItem(_applicationDbContext.SalesItemMaster.Where(x => x.IsActive == true).ToList());
        }

        public SalesItemViewModel GetSalesItem(int id)
        {
            return SalesItemMap.GetAllSalesItem(_applicationDbContext.SalesItemMaster.FirstOrDefault(x => x.Id == id));
        }

        public int AddSalesItem(SalesItemViewModel item)
        {
            SalesItemMaster salesItemMaster = SalesItemMap.MapCreateSalesItem(item);

            var entityEntry = _applicationDbContext.SalesItemMaster.Add(salesItemMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteSalesItem(int id)
        {
            var salesItem = _applicationDbContext.SalesItemMaster.FirstOrDefault(x => x.Id == id);
            if (salesItem != null)
            {
                salesItem.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;

            }
            return false;

        }

        public bool UpdateSalesItem(SalesItemViewModel item)
        {
            using (var dbContext = _applicationDbContext)
            {
                var Salesitem = dbContext.SalesItemMaster.FirstOrDefault(x=>x.Id == item.Id);

                if (Salesitem != null)
                {
                    Salesitem.SalesmasterId = item.SalesmasterId;
                    Salesitem.ItemID = item.ItemID;
                    Salesitem.Quantity = item.Quantity;
                    Salesitem.Mrp = item.Mrp;
                    Salesitem.DiscountPercentage = item.DiscountPercentage;
                    Salesitem.TotalPrice = item.TotalPrice;
                    Salesitem.IsActive = true;
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}

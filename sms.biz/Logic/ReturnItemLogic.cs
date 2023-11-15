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
    public class ReturnItemLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReturnItemLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<ReturnItemViewModel> GetReturnItems()
        {
            return ReturnItemMap.MapGetReturnItem(_applicationDbContext.ReturnItemMaster.Where(x=>x.IsActive==true).ToList());
        }

        public ReturnItemViewModel GetReturnItem(int id)
        {
            return ReturnItemMap.GetAllReturnItem(_applicationDbContext.ReturnItemMaster.FirstOrDefault(x=>x.Id==id));
        }

        public int AddReturnItem(ReturnItemViewModel item)
        {
            ReturnItemMaster returnItemMaster = ReturnItemMap.MapCreateReturnItem(item);

            var entityEntry = _applicationDbContext.ReturnItemMaster.Add(returnItemMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteReturnItem(int id)
        {
            var returnItem = _applicationDbContext.ReturnItemMaster.FirstOrDefault(x => x.Id == id);
            if (returnItem != null)
            {
                returnItem.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateReturnItem(ReturnItemViewModel item)
        {
            using (var context = _applicationDbContext) 
            { 
               var ReturnItemMaster = context.ReturnItemMaster.FirstOrDefault(x=> x.Id == item.Id);
                if(ReturnItemMaster != null)
                {
                    ReturnItemMaster.ReturnMasterId = item.ReturnMasterId;
                    ReturnItemMaster.ItemID = item.ItemID;
                    ReturnItemMaster.Mrp = item.Mrp;
                    ReturnItemMaster.Total = item.Total;
                    ReturnItemMaster.IsActive = true;

                    context.SaveChanges();
                    return true;
                }
                return false;
            
            }
        }
    }
}

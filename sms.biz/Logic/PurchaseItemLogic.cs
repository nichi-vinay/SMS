using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public  class PurchaseItemLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseItemLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<PurchaseItemViewModel> GetAllPurchaseItem()
        {
            return PurchaseItemMap.MapPurchaseItem(_applicationDbContext.PurchaseItemMaster.Where(x=>x.IsActive==true).ToList(), _applicationDbContext);
        }

        public PurchaseItemViewModel GetPurchaseItem(int id)
        {
            return PurchaseItemMap.GetallPurchaseItemDetails(_applicationDbContext.PurchaseItemMaster.FirstOrDefault(x=>x.Id==id), _applicationDbContext);
        }

        public int AddPurchaseItem(PurchaseItemViewModel item)
        {
            PurchaseItemMaster purchaseItemMaster = PurchaseItemMap.MapCreatePurchase(item);


            var EntityEntry = _applicationDbContext.PurchaseItemMaster.Add(purchaseItemMaster);

            return EntityEntry.Entity.Id;
        }

        public bool DeletePurchaseItem(int id) 
        { 
            var purchaseItem = _applicationDbContext.PurchaseItemMaster.FirstOrDefault(x=> x.Id==id);
            if (purchaseItem != null)
            {
                purchaseItem.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdatePurchaseItem(PurchaseItemViewModel item)
        {
            using(var  db = _applicationDbContext)
            {
                var purchaseItemmaster = db.PurchaseItemMaster.FirstOrDefault(x=>x.Id == item.Id);
                if (purchaseItemmaster != null)
                {
                    purchaseItemmaster.ItemID = item.Id;
                    purchaseItemmaster.PurchasemasterId = item.PurchasemasterId;
                    purchaseItemmaster.Quantity = item.Quantity;
                    //purchaseItemmaster.IsSubmitted=item.IsSubmitted;
                    purchaseItemmaster.Mrp=item.Mrp;
                    purchaseItemmaster.DiscountPercentage = item.DiscountPercentage;
                    purchaseItemmaster.Totalprice = item.Totalprice;
                    purchaseItemmaster.IsActive = true;
                    purchaseItemmaster.Barcode = item.Barcode;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}

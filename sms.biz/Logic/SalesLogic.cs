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
    public class SalesLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SalesLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<SalesViewModel> GetAllSales()
        {
            return SalesMap.MapGetSales(_applicationDbContext.SalesMaster.Where(x=>x.IsActive==true).ToList());
        }

        public SalesViewModel GetSales(int id)
        {
            return SalesMap.GetAllSales(_applicationDbContext.SalesMaster.FirstOrDefault(x=>x.Id==id));
        }

        public int AddSales(SalesViewModel item)
        {
            SalesMaster salesMaster = SalesMap.MapCreateSales(item);

            var entityEntry = _applicationDbContext.SalesMaster.Add(salesMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }
        public bool DeleteSales(int id)
        {
            var sales = _applicationDbContext.SalesMaster.FirstOrDefault(x => x.Id == id);
            if (sales != null)
            {
                sales.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;

            }
            return false;
        }

        public bool UpdateSales(SalesViewModel item)
        {
            using (var db =  _applicationDbContext)
            {
                var sales = db.SalesMaster.FirstOrDefault(x=> x.Id == item.Id);
                if(sales != null)
                {
                    sales.CustomerId = item.CustomerId;
                    sales.customerTypeMasterId = item.customerTypeMasterId;
                    sales.InvoiceCopy = item.InvoiceCopy;
                    sales.InvoiceDate = item.InvoiceDate;
                    sales.InvoiceNumber = item.InvoiceNumber;
                    sales.ShipmentDetails = item.ShipmentDetails;
                    sales.Cards = item.Cards;
                    sales.Cash = item.Cash;
                    sales.Cheque = item.Cheque;
                    sales.Online = item.Online;
                    sales.TaxNumber = item.TaxNumber;
                    sales.ExpectedDelivery = item.ExpectedDelivery;
                    sales.IsCanceled = item.IsCanceled;
                    sales.IsActive = true;
                    _applicationDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}

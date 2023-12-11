using Microsoft.EntityFrameworkCore;
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
            return SalesMap.MapGetSales(_applicationDbContext.SalesMaster.Where(x=>x.IsActive==true).ToList(), _applicationDbContext);
        }

        public SalesViewModel GetSales(int id)
        {
            return SalesMap.GetAllSales(_applicationDbContext.SalesMaster.FirstOrDefault(x=>x.Id==id), _applicationDbContext);
        }

        public int AddSales(SalesViewModel item, List<SalesItemViewModel> Salesitems)
        {
            SalesMaster salesMaster = SalesMap.MapCreateSales(item, Salesitems,_applicationDbContext);

            _applicationDbContext.SaveChanges();

            return salesMaster.Id;
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

        public bool UpdateSales(SalesViewModel item, List<SalesItemViewModel> SalesItem)
        {
            var existingSalesMaster = _applicationDbContext.SalesMaster
    .Include(sm => sm.salesItemMasters)
    .Include(sm => sm.SalesTransactionsMaster) // Include SalesTransactionsMaster here
    .FirstOrDefault(x => x.Id == item.Id);

            if (existingSalesMaster != null)
            {
                // Update SalesMaster properties
                existingSalesMaster.CustomerId = item.CustomerId;
                existingSalesMaster.customerTypeMasterId = item.customerTypeMasterId;
                existingSalesMaster.InvoiceCopy = item.InvoiceCopy;
                existingSalesMaster.InvoiceDate = item.InvoiceDate;
                existingSalesMaster.InvoiceNumber = item.InvoiceNumber;
                existingSalesMaster.ShipmentDetails = item.ShipmentDetails;
                existingSalesMaster.SalesTransactionsMaster.SalesmasterId = item.Id;
                existingSalesMaster.SalesTransactionsMaster.Cards = item.Cards;
                existingSalesMaster.SalesTransactionsMaster.Cash = item.Cash;
                existingSalesMaster.SalesTransactionsMaster.Cheque = item.Cheque;
                existingSalesMaster.SalesTransactionsMaster.Online = item.Online;
                existingSalesMaster.SalesTransactionsMaster.TotalPaid = item.TotalPaid;
                existingSalesMaster.TaxNumber = item.TaxNumber;
                existingSalesMaster.ExpectedDelivery = item.ExpectedDelivery;
                existingSalesMaster.IsCanceled = item.IsCanceled;
                existingSalesMaster.IsActive = true;

                // Update or add SalesItemMasters
                foreach (var salesItem in SalesItem)
                {
                    var existingSalesItem = existingSalesMaster.salesItemMasters.FirstOrDefault(s => s.Id == salesItem.Id);
                    if (existingSalesItem != null)
                    {
                        existingSalesItem.Id = salesItem.Id;
                        // Update existing SalesItemMaster
                        existingSalesItem.ItemID = salesItem.ItemID;
                        existingSalesItem.TaxTypeID = salesItem.TaxTypeID;
                        existingSalesItem.Quantity = salesItem.Quantity;
                        existingSalesItem.Mrp = salesItem.Mrp;
                        existingSalesItem.TaxPercentage = salesItem.TaxPercentage;
                        existingSalesItem.TotalPrice = salesItem.TotalPrice;
                        existingSalesItem.Barcode = salesItem.Barcode;
                        existingSalesItem.IsActive = true;
                        existingSalesItem.CreatedBy = 1;
                        existingSalesItem.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        // Add new SalesItemMaster
                        existingSalesMaster.salesItemMasters.Add(new SalesItemMaster
                        {
                            SalesmasterId = existingSalesMaster.Id,
                            ItemID = salesItem.ItemID,
                            TaxTypeID = salesItem.TaxTypeID,
                            Quantity = salesItem.Quantity,
                            Mrp = salesItem.Mrp,
                            TaxPercentage = salesItem.TaxPercentage,
                            TotalPrice = salesItem.TotalPrice,
                            Barcode = salesItem.Barcode,
                            IsActive = true,
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now
                        });
                    }
                }

                // Save changes to the database
                _applicationDbContext.SaveChanges();

                return true;
            }

            return false;
        }

    }
}

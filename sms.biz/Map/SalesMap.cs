using Microsoft.EntityFrameworkCore;
using sms.data;
using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class SalesMap
    {
        public static SalesViewModel GetAllSales(this SalesMaster salesMaster, ApplicationDbContext context)
        {
            var query = context.SalesMaster
         .Include(sm => sm.salesItemMasters)
         .Include(sim =>sim.SalesTransactionsMasters)
         .Where(sm => sm.Id == salesMaster.Id);
            var result = query.Select(sm=>new  SalesViewModel
            {
                Id = sm.Id,
                CustomerId = sm.CustomerId,
                customerTypeMasterId = sm.customerTypeMasterId,
                InvoiceCopy = sm.InvoiceCopy,
                InvoiceDate = sm.InvoiceDate.ToString("dd-MM-yyyy"),
                InvoiceNumber = sm.InvoiceNumber,
                totaltax   = sm.TotalTax,
                TotaDiscount = sm.TotalDiscount,
                TotalMRP = sm.TotalMrp,
            
                TotalAmount = sm.TotalAmount,
                ShipmentDetails = sm.ShipmentDetails,
                ExpectedDelivery = sm.ExpectedDelivery.ToString("dd-MM-yyyy"),
          
                IsActive= sm.IsActive,
                IsCanceled= sm.IsCanceled,
                TaxNumber = sm.TaxNumber,
                SalesItems = sm.salesItemMasters.Select(si=>new SalesItemViewModel
                {
                    Id=si.Id,
                    SalesmasterId=si.SalesmasterId,
                    ItemID=si.ItemID,
                    TaxTypeID=si.TaxTypeID,
                    Quantity=si.Quantity,
                    Mrp = si.Mrp,
                    TaxPercentage =si.TaxPercentage,
                    TotalPrice=si.TotalPrice,
                    Barcode=si.Barcode,
                }).ToList(),
                SalesTransactions =sm.SalesTransactionsMasters.Select(ss=>new SalesTransactionViewModel

                {
                    Id = ss.Id,
                    SalesmasterId = ss.SalesmasterId,
                    Cards = ss.Cards,
                    Cash = ss.Cash,
                    Cheque = ss.Cheque,
                    Online = ss.Online,
                    TotalPaid = ss.TotalPaid
                }).ToList()

            }).FirstOrDefault();
        
     
            return result ?? new SalesViewModel();

        }

        public static List<SalesViewModel> MapGetSales(this List<SalesMaster> salesMasterList, ApplicationDbContext context)
        {
            return salesMasterList.Select(x => x.GetAllSales(context)).ToList();
        }

        public static SalesMaster MapCreateSales(this SalesViewModel salesMaster, List<SalesItemViewModel> SalesItem,ApplicationDbContext context,List<SalesTransactionViewModel>salesTransactionViews)
        {
            string latestInvoiceNumber = context.SalesMaster
        .Where(x => x.IsActive)
        .OrderByDescending(x => x.CreatedDate)
        .Select(x => x.InvoiceNumber)
        .FirstOrDefault();
            int nextInvoiceNumber = string.IsNullOrEmpty(latestInvoiceNumber)
        ? 1
        : int.Parse(latestInvoiceNumber) + 1;

            // Format the next invoice number with leading zeros
            string formattedInvoiceNumber = nextInvoiceNumber.ToString("D3");

            var salesMasters = new SalesMaster
            {
                CustomerId = salesMaster.CustomerId,
                customerTypeMasterId = salesMaster.customerTypeMasterId,
                InvoiceNumber = formattedInvoiceNumber,
                InvoiceDate = DateTime.Today,
                ShipmentDetails = salesMaster.ShipmentDetails,
                TotalDiscount = salesMaster.TotaDiscount,
                TaxNumber = salesMaster.TaxNumber,
                TotalAmount = salesMaster.TotalAmount,              
                TotalTax = salesMaster.totaltax,
                InvoiceCopy = salesMaster.InvoiceCopy,
                ExpectedDelivery = Convert.ToDateTime(salesMaster.ExpectedDelivery),
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
                IsCanceled = salesMaster.IsCanceled,
                TotalMrp = salesMaster.TotalMRP

            };
          
            var savedSalesMaster = context.SalesMaster.Add(salesMasters);
            context.SaveChanges();
            int newSalesMasterId = savedSalesMaster.Entity.Id;
           
            var SalesItemMaster = SalesItem.Select(item => new SalesItemMaster
            {
                SalesmasterId = newSalesMasterId,
                ItemID = item.ItemID,
                TaxTypeID = item.TaxTypeID,
                Quantity = item.Quantity,
                Mrp = item.Mrp,
                TaxPercentage = item.TaxPercentage,
                TotalPrice = item.TotalPrice,
                Barcode = item.Barcode,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now

            }).ToList();
            salesMasters.salesItemMasters = SalesItemMaster;
            var salesTransaction = salesTransactionViews.Select(i=>new SalesTransactionsMaster
            {
                Cards = i.Cards,
                Cash = i.Cash,
                Cheque = i.Cheque,
                Online = i.Online,
                TotalPaid = i.TotalPaid,
                SalesmasterId = newSalesMasterId,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            }).ToList();
            salesMasters.SalesTransactionsMasters = salesTransaction;
            return salesMasters;

        }

       
    }
}

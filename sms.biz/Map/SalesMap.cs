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
         .Where(sm => sm.Id == salesMaster.Id);
            var result = query.Select(sm=>new  SalesViewModel
            {
                Id = sm.Id,
                CustomerId = sm.CustomerId,
                customerTypeMasterId = sm.customerTypeMasterId,
                InvoiceCopy = sm.InvoiceCopy,
                InvoiceDate = sm.InvoiceDate,
                InvoiceNumber = sm.InvoiceNumber,
                totaltax   = sm.TotalTax,
                TotaDiscount = sm.TotalDiscount,
                TotalMRP = sm.TotalMrp,
                TotalPaid = sm.TotalPaid,
                TotalAmount = sm.TotalAmount,
                ShipmentDetails = sm.ShipmentDetails,
                ExpectedDelivery = sm.ExpectedDelivery,
                Cards = sm.Cards,
                Cash= sm.Cash,
                Cheque= sm.Cheque,
                Online= sm.Online,
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
                    DiscountPercentage=si.DiscountPercentage,
                    TotalPrice=si.TotalPrice,
                    Barcode=si.Barcode,
                }).ToList(),



            }).FirstOrDefault();
        
     
            return result ?? new SalesViewModel();

        }

        public static List<SalesViewModel> MapGetSales(this List<SalesMaster> salesMasterList, ApplicationDbContext context)
        {
            return salesMasterList.Select(x => x.GetAllSales(context)).ToList();
        }

        public static SalesMaster MapCreateSales(this SalesViewModel salesMaster, List<SalesItemViewModel> SalesItem,ApplicationDbContext context)
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
                InvoiceDate = salesMaster.InvoiceDate,
                ShipmentDetails = salesMaster.ShipmentDetails,
                TotalDiscount = salesMaster.TotaDiscount,
                TaxNumber = salesMaster.TaxNumber,
                TotalAmount = salesMaster.TotalAmount,
                TotalPaid = salesMaster.TotalPaid,
                TotalTax = salesMaster.totaltax,
                InvoiceCopy = salesMaster.InvoiceCopy,
                ExpectedDelivery = salesMaster.ExpectedDelivery,
                Cheque = salesMaster.Cheque,
                Cash = salesMaster.Cash,
                Cards = salesMaster.Cards,
                Online = salesMaster.Online,
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
                Mrp= item.Mrp,
                DiscountPercentage = item.DiscountPercentage,
                TotalPrice = item.TotalPrice,
                Barcode = item.Barcode,
                IsActive = true,
                CreatedBy =1,
                CreatedDate = System.DateTime.Now

            }).ToList();

            salesMasters.salesItemMasters = SalesItemMaster;
            return salesMasters;

        }

       
    }
}

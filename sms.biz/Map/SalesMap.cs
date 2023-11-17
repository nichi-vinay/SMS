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
        public static SalesViewModel GetAllSales(this SalesMaster salesMaster)
        {
            SalesViewModel viewModel = new SalesViewModel
            {
                Id = salesMaster.Id,
                CustomerId = salesMaster.CustomerId,
                customerTypeMasterId = salesMaster.customerTypeMasterId,
                InvoiceNumber = salesMaster.InvoiceNumber,
                InvoiceDate = salesMaster.InvoiceDate,
                ShipmentDetails = salesMaster.ShipmentDetails,
                InvoiceCopy = salesMaster.InvoiceCopy,
                IsCanceled = salesMaster.IsCanceled,
                IsActive = salesMaster.IsActive,
                ExpectedDelivery = salesMaster.ExpectedDelivery,
                Cards = salesMaster.Cards,
                Cash  = salesMaster.Cash,
                Cheque = salesMaster.Cheque,
                Online = salesMaster.Online,
                TaxNumber = salesMaster.TaxNumber,
                CreatedBy = salesMaster.CreatedBy,
                CreatedDate = salesMaster.CreatedDate,
            };
            return viewModel;
        }

        public static List<SalesViewModel>MapGetSales(this List<SalesMaster> salesMasterList)
        {
            return salesMasterList.Select(x=>x.GetAllSales()).ToList();
        }

        public static SalesMaster MapCreateSales(this SalesViewModel viewModel)
        {
            return new SalesMaster
            {
                Id = viewModel.Id,
                CustomerId = viewModel.CustomerId,
                customerTypeMasterId = viewModel.customerTypeMasterId,
                InvoiceNumber = viewModel.InvoiceNumber,
                InvoiceDate = viewModel.InvoiceDate,
                ShipmentDetails = viewModel.ShipmentDetails,
                InvoiceCopy = viewModel.InvoiceCopy,
                IsCanceled = viewModel.IsCanceled,
                Cards = viewModel.Cards,
                Cash = viewModel.Cash,
                Cheque= viewModel.Cheque,
                Online = viewModel.Online,
                TaxNumber = viewModel.TaxNumber,
                ExpectedDelivery = viewModel.ExpectedDelivery,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
            };
        }
    }
}

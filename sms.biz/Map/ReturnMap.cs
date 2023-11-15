using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class ReturnMap
    {
        public static ReturnViewModel GetAllReturns(this ReturnMaster returnMaster)
        {
            ReturnViewModel returnViewModel = new ReturnViewModel
            {
                Id = returnMaster.Id,
                VendorId = returnMaster.VendorId,
                InvoiceName = returnMaster.InvoiceName,
                ReturnNumber = returnMaster.ReturnNumber,
                ReturnDate = returnMaster.ReturnDate,
                ShipmentDetails = returnMaster.ShipmentDetails,
                TaxNumber = returnMaster.TaxNumber,
                IsCanceled = returnMaster.IsCanceled,
                IsActive = returnMaster.IsActive,
                CreatedBy = returnMaster.CreatedBy,
                CreatedDate = returnMaster.CreatedDate,
            };
            return returnViewModel;

        }

        public static List<ReturnViewModel>MapGetReturn(this List<ReturnMaster> returnMaster) 
        {
            return returnMaster.Select(x=>x.GetAllReturns()).ToList();
        }

        public static ReturnMaster MapCreateReturn(this ReturnViewModel returnViewModel)
        {
            return new ReturnMaster
            {
                Id = returnViewModel.Id,

                VendorId = returnViewModel.VendorId,
                InvoiceName = returnViewModel.InvoiceName,
                ReturnNumber = returnViewModel.ReturnNumber,
                ReturnDate = returnViewModel.ReturnDate,
                ShipmentDetails = returnViewModel.ShipmentDetails,
                TaxNumber = returnViewModel.TaxNumber,
                IsCanceled = returnViewModel.IsCanceled,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
            };
        }
    }
}

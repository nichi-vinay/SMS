using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class VendorMap
    {
        public static VendorViewModel GetVendorDetails(this VendorMaster vendorMaster)
        {
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Id = vendorMaster.Id,
                Name = vendorMaster.Name,
                Address = vendorMaster.Address,
                Email = vendorMaster.Email,
                Mobile = vendorMaster.Mobile,
                TaxNumber = vendorMaster.TaxNumber,               
                TaxTypeMasterId = vendorMaster.TaxTypeMasterId,
                IsActive = vendorMaster.IsActive,
                CreatedBy = vendorMaster.CreatedBy,
                CreatedDate = vendorMaster.CreatedDate

            };
            return vendorViewModel;
        }
        
        public static List<VendorViewModel>MapGetVendor(this List<VendorMaster> vendorMasterList)
        {
            return vendorMasterList.Select(x=>x.GetVendorDetails()).ToList();
        }

        public static VendorMaster MapCreateVendor(this VendorViewModel vendorViewModel) 
        {
            return new VendorMaster 
            { 
                Id=vendorViewModel.Id,
                Name = vendorViewModel.Name,
                Address = vendorViewModel.Address,
                Email = vendorViewModel.Email,
                Mobile = vendorViewModel.Mobile,
                TaxNumber = vendorViewModel.TaxNumber,
                TaxTypeMasterId= vendorViewModel.TaxTypeMasterId,
                IsActive = vendorViewModel.IsActive,
                CreatedBy=1,
                CreatedDate = System.DateTime.Now,
            };
        }

    }
}

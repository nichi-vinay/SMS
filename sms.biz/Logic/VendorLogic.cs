using Microsoft.EntityFrameworkCore;
using sms.biz.Map;
using sms.data;
using sms.data.Models;
using sms.viewmodels;
using System.Collections.Generic;
using System.Linq;

namespace sms.biz.Logic
{
    public class VendorLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VendorLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<VendorViewModel> GetAllVendors()
        {
            return VendorMap.MapGetVendor(_applicationDbContext.VendorMaster.Where(x => x.IsActive == true).ToList());
        }

        public VendorViewModel GetVendor(int id)
        {
            var data = VendorMap.GetVendorDetails(_applicationDbContext.VendorMaster.FirstOrDefault(x => x.Id == id));
            return data;
        }

        public int AddVendor(VendorViewModel vendor)
        {
            VendorMaster vendorMaster = VendorMap.MapCreateVendor(vendor);

            var entityEntry = _applicationDbContext.VendorMaster.Add(vendorMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteVendor(int id)
        {
            var vendor = _applicationDbContext.VendorMaster.FirstOrDefault(x => x.Id == id);

            if (vendor != null)
            {
                vendor.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }

            return false;
        }
        public bool UpdateVendor(VendorViewModel vendor)
        {
            using (var dbContext = _applicationDbContext)
            {
                var vendorMaster = dbContext.VendorMaster.FirstOrDefault(x => x.Id == vendor.Id);
                if (vendorMaster != null)
                {
                    vendorMaster.Name = vendor.Name;
                    vendorMaster.Address = vendor.Address;
                    vendorMaster.Email = vendor.Email;
                    vendorMaster.Mobile = vendor.Mobile;
                    vendorMaster.TaxTypeMasterId = vendor.TaxTypeMasterId;
                    vendorMaster.TaxNumber = vendor.TaxNumber;
                    vendorMaster.IsActive = true;

                    dbContext.SaveChanges(); // Save the changes to the database
                    return true;
                }
            }
            return false; // Return false if the vendor with the given ID was not found
        }
    }

    }

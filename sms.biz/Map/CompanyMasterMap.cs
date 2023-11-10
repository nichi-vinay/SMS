using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class CompanyMasterMap
    {
        public static CompanyMasterViewModel GetAllCompanyDetails(this CompanyMaster companyMaster)
        {
            CompanyMasterViewModel viewModel = new CompanyMasterViewModel()
            {
                Id = companyMaster.Id,
                ItemID = companyMaster.ItemID,
                Logo = companyMaster.Logo,
                Name = companyMaster.Name,
                Mrp = companyMaster.Mrp,
                TotalPrice = companyMaster.TotalPrice,
                IsActive = companyMaster.IsActive,
            };
            return viewModel;
        }

        public static List<CompanyMasterViewModel>MapGetCompany(this List<CompanyMaster> companyMaster)
        {
            return companyMaster.Select(x=>x.GetAllCompanyDetails()).ToList();

        }

        public static CompanyMaster MApCreateCompany(this CompanyMasterViewModel companyMasterView)
        {
            return new CompanyMaster
            {
                Id=companyMasterView.Id,
                Name=companyMasterView.Name,
                ItemID=companyMasterView.ItemID,
                Logo = companyMasterView.Logo,
                Mrp=companyMasterView.Mrp,
                TotalPrice = companyMasterView.TotalPrice,
                IsActive = companyMasterView.IsActive,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

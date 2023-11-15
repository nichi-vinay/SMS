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
    public class CompanyMasterLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CompanyMasterLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<CompanyMasterViewModel>GetAllComapnyDetails()
        {
            return CompanyMasterMap.MapGetCompany(_applicationDbContext.CompanyMaster.Where(x=>x.IsActive==true).ToList());
        }

        public CompanyMasterViewModel GetCompany(int id)
        {
            return CompanyMasterMap.GetAllCompanyDetails(_applicationDbContext.CompanyMaster.FirstOrDefault(x=>x.Id==id));
        }

        public int AddCompany(CompanyMasterViewModel company)
        {
            CompanyMaster companyMaster = CompanyMasterMap.MApCreateCompany(company);
            
            var EntityEntry = _applicationDbContext.CompanyMaster.Add(companyMaster);
            _applicationDbContext.SaveChanges();
            return EntityEntry.Entity.Id;
        }

        public bool DeleteCompany(int id)
        {
            var company = _applicationDbContext.CompanyMaster.FirstOrDefault(x => x.Id==id);
            if (company != null)
            {
                company.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateCompany(CompanyMasterViewModel company)
        {     
                var companyMaster = _applicationDbContext.CompanyMaster.FirstOrDefault(x=> x.Id==company.Id);
                if (companyMaster != null)
                {
                    companyMaster.Name = company.Name;
                    companyMaster.ItemID = company.ItemID;
                    companyMaster.Logo = company.Logo;
                    companyMaster.Mrp = company.Mrp;
                    companyMaster.TotalPrice = company.TotalPrice;
                    companyMaster.IsActive = true;

                    _applicationDbContext.SaveChanges();
                return true;
                }
            return false;  
        }
    }
}

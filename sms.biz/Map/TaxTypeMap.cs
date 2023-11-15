using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class TaxTypeMap
    {
        public static TaxTypeViewModel GetAllTaxtype(this TaxTypeMaster taxTypeMaster)
        {
            TaxTypeViewModel taxTypeViewModel = new TaxTypeViewModel
            {
                Id = taxTypeMaster.Id,
                Name = taxTypeMaster.Name,
                IsActive = taxTypeMaster.IsActive,
                CreatedBy = taxTypeMaster.CreatedBy,
                CreatedDate = taxTypeMaster.CreatedDate,
            };
            return taxTypeViewModel;
        }

        public static List<TaxTypeViewModel>MapGetTaxType(this List<TaxTypeMaster> taxTypeMasterList)
        {
            return taxTypeMasterList.Select(x=>x.GetAllTaxtype()).ToList();
        }

        public static TaxTypeMaster MapCreateTaxType(this TaxTypeViewModel taxTypeViewModel)
        {
            return new TaxTypeMaster
            {
                Id = taxTypeViewModel.Id,
                Name = taxTypeViewModel.Name,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class CustomerTypeMap
    {
        public static CustomerTypeViewModel GetAllCustomerTypeDetails(this CustomerTypeMaster customerTypeMaster)
        {
            CustomerTypeViewModel customerTypeMasterViewModel = new CustomerTypeViewModel()
            {
                Id = customerTypeMaster.Id,
                CustomerTypeName = customerTypeMaster.CustomerTypeName,
                CreatedBy = customerTypeMaster.CreatedBy,
                CreatedDate = customerTypeMaster.CreatedDate
            };
            return customerTypeMasterViewModel;
        }

        public static List<CustomerTypeViewModel>MapGetCustomerType(this List<CustomerTypeMaster> customerTypeMasterList)
        {
            return customerTypeMasterList.Select(x => x.GetAllCustomerTypeDetails()).ToList();
        }

        public static CustomerTypeMaster MapCreateCustomerType(this CustomerTypeViewModel customerTypeViewModel)
        {
            return new CustomerTypeMaster
            {
                Id = customerTypeViewModel.Id,
                CustomerTypeName = customerTypeViewModel.CustomerTypeName,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }

    }
}

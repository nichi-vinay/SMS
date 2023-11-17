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
    public class CustomerTypeLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CustomerTypeLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<CustomerTypeViewModel> GetAllCustomerTypeList()
        {
            return CustomerTypeMap.MapGetCustomerType(_applicationDbContext.CustomerTypeMaster.ToList());
        }

        public CustomerTypeViewModel GetCustomerType(int id)
        {
            return CustomerTypeMap.GetAllCustomerTypeDetails(_applicationDbContext.CustomerTypeMaster.FirstOrDefault(x => x.Id == id));
        }

        public int AddCustomerType(CustomerTypeViewModel customerType)
        {
            CustomerTypeMaster customerTypeMaster = CustomerTypeMap.MapCreateCustomerType(customerType);

            var entityEntry = _applicationDbContext.CustomerTypeMaster.Add(customerTypeMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteCustomerType(int id)
        {
            var customerType = _applicationDbContext.CustomerTypeMaster.FirstOrDefault(x=>x.Id == id);
            if(customerType!=null)
            {
                var entity = _applicationDbContext.CustomerTypeMaster.Remove(customerType);
                _applicationDbContext.SaveChanges();
                
                return true;
            }
            return false;

        }


    }
}

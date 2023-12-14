using Microsoft.EntityFrameworkCore;
using sms.biz.Map;
using sms.data;
using sms.data.Models;
using sms.viewmodels;
using System.Collections.Generic;
using System.Linq;


namespace sms.biz.Logic
{
    public class CustomerLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CustomerLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<CustomerViewModel> GetAllCustomers()
        {
            return CustomerMap.MapGetCustomer(_applicationDbContext.CustomerMaster.Where(x => x.IsActive == true).ToList(), _applicationDbContext);
        }

        public CustomerViewModel GetCustomer(int id)
        {
            var data = CustomerMap.GetCustomerDetails(_applicationDbContext.CustomerMaster.FirstOrDefault(x => x.Id == id), _applicationDbContext);
            return data;
        }

        public int AddCustomer(CustomerViewModel customer)
        {
            CustomerMaster customerMaster = CustomerMap.MapCreateCustomer(customer);

            var entityEntry = _applicationDbContext.CustomerMaster.Add(customerMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteCustomer(int id)
        {
            var customer = _applicationDbContext.CustomerMaster.FirstOrDefault(x => x.Id == id);

            if (customer != null)
            {
                customer.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UpdateCustomer(CustomerViewModel customer)
        {
            CustomerMaster customerMaster = new CustomerMaster();
            using (var dbContext = _applicationDbContext)
            {
                customerMaster = dbContext.CustomerMaster.FirstOrDefault(x => x.Id == customer.Id);
                customerMaster.Name = customer.Name;
                customerMaster.Address = customer.Address;
                customerMaster.Email = customer.Email;
                customerMaster.Mobile = customer.Mobile;
                customerMaster.Comments = customer.Comments;
                customerMaster.FollowUpdate = customer.FollowUpdate;
                customerMaster.IsWhatsapp = customer.IsWhatsapp;
                customerMaster.IsActive = true;
                customerMaster.EnquirytypeId = customer.EnquirytypeId;


                dbContext.SaveChanges();
            }
            return true;
        }
    }
}

using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class CustomerMap
    {
        /// <summary>
        /// Converts a CustomerMaster model to a CustomerViewModel instance.
        /// </summary>
        /// <param name="customerMaster">The CustomerMaster model to map.</param>
        /// <returns>A CustomerViewModel instance representing the provided CustomerMaster model.</returns>
        public static CustomerViewModel GetCustomerDetails(this CustomerMaster customerMaster)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Id = customerMaster.Id,
                Name = customerMaster.Name,
                Address = customerMaster.Address,
                Email = customerMaster.Email,
                Mobile = customerMaster.Mobile,
                Comments = customerMaster.Comments,
                FollowUpdate = customerMaster.FollowUpdate,
                IsWhatsapp = customerMaster.IsWhatsapp,
                IsActive = customerMaster.IsActive,
                CreatedBy = customerMaster.CreatedBy,
                CreatedDate = customerMaster.CreatedDate,
                EnquirytypeId = customerMaster.EnquirytypeId
            };
            return customerViewModel;
        }

        /// <summary>
        /// Converts a list of CustomerMaster models to a list of CustomerViewModel instances.
        /// </summary>
        /// <param name="customerMasterList">The list of CustomerMaster models to map.</param>
        /// <returns>A list of CustomerViewModel instances representing the provided list of CustomerMaster models.</returns>
        public static List<CustomerViewModel> MapGetCustomer(this List<CustomerMaster> customerMasterList)
        {
            return customerMasterList.Select(x => x.GetCustomerDetails()).ToList();
        }

        /// <summary>
        /// Converts a CustomerViewModel instance to a CustomerMaster model.
        /// </summary>
        /// <param name="customerViewModel">The CustomerViewModel instance to map.</param>
        /// <returns>A CustomerMaster model representing the provided CustomerViewModel instance.</returns>
        public static CustomerMaster MapCreateCustomer(this CustomerViewModel customerViewModel)
        {
            return new CustomerMaster
            {
                Id = customerViewModel.Id,
                Name = customerViewModel.Name,
                Address = customerViewModel.Address,
                Email = customerViewModel.Email,
                Mobile = customerViewModel.Mobile,
                Comments = customerViewModel.Comments,
                FollowUpdate = customerViewModel.FollowUpdate,
                IsWhatsapp = customerViewModel.IsWhatsapp,
                IsActive = customerViewModel.IsActive,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
                EnquirytypeId = customerViewModel.EnquirytypeId
            };
        }
    }
}

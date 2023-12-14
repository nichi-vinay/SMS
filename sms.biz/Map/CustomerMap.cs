using sms.data;
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
        public static CustomerViewModel GetCustomerDetails(this CustomerMaster customerMaster, ApplicationDbContext context)
        {
            var query = from cm in context.CustomerMaster
                        //join etm in context.EnquiryTypeMaster on cm.EnquirytypeId equals etm.Id
                        join sm in context.SalesMaster on cm.Id equals sm.CustomerId into salesGroup
                        from sm in salesGroup.DefaultIfEmpty()
                        where cm.Id == customerMaster.Id
                        select new CustomerViewModel
                        {
                            Id = cm.Id,
                            Name = cm.Name,
                            Address = cm.Address,
                            Email = cm.Email,
                            Mobile = cm.Mobile,
                            Comments = cm.Comments,
                            FollowUpdate = cm.FollowUpdate.Date,
                            IsWhatsapp = cm.IsWhatsapp,
                            IsActive = cm.IsActive,
                            IsCustomer = cm.IsCustomer,
                            CreatedBy = cm.CreatedBy,
                            CreatedDate = cm.CreatedDate,
                            EnquirytypeId = cm.EnquirytypeId,
                            EnquiryTypeName = cm.EnquiryTypeMaster.EnquiryTypeName,
                            InvoiceNumber = sm != null ? sm.InvoiceNumber : null
                        };
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Converts a list of CustomerMaster models to a list of CustomerViewModel instances.
        /// </summary>
        /// <param name="customerMasterList">The list of CustomerMaster models to map.</param>
        /// <returns>A list of CustomerViewModel instances representing the provided list of CustomerMaster models.</returns>
        public static List<CustomerViewModel> MapGetCustomer(this List<CustomerMaster> customerMasterList, ApplicationDbContext context)
        {
            return customerMasterList.Select(x => x.GetCustomerDetails(context)).ToList();
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
            
                Name = customerViewModel.Name,
                Address = customerViewModel.Address,
                Email = customerViewModel.Email,
                Mobile = customerViewModel.Mobile,
                Comments = customerViewModel.Comments,
                FollowUpdate = customerViewModel.FollowUpdate,
                IsWhatsapp = customerViewModel.IsWhatsapp,
                IsCustomer = customerViewModel.IsCustomer,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
                EnquirytypeId = customerViewModel.EnquirytypeId,
            };
        }
    }
}

using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class OrderMap
    {
        /// <summary>
            /// Converts an OrderMaster model to an OrderViewModel instance.
            /// </summary>
            /// <param name="orderMaster">The OrderMaster model to map.</param>
            /// <returns>An OrderViewModel instance representing the provided OrderMaster model.</returns>
        public static OrderViewModel GetOrderDetails(this OrderMaster orderMaster)
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Id = orderMaster.Id,
                VendorId = orderMaster.VendorId,
                OrderTypeId = orderMaster.OrderTypeId,
                OrderName = orderMaster.OrderName,
                OrderNumber = orderMaster.OrderNumber,
                OrderDate = orderMaster.OrderDate,
                ShipmentDetails = orderMaster.ShipmentDetails,
                IsCancelled = orderMaster.IsCancelled,
                IsActive = orderMaster.IsActive,
                CreatedBy = orderMaster.CreatedBy,
                CreatedDate = orderMaster.CreatedDate,
                ModifiedDate = orderMaster.ModifiedDate,
                ModifiedBy = orderMaster.ModifiedBy,

            };
            return orderViewModel;
        }

        /// <summary>
            /// Converts a list of OrderMaster models to a list of OrderViewModel instances.
            /// </summary>
            /// <param name="orderMasterList">The list of OrderMaster models to map.</param>
            /// <returns>A list of OrderViewModel instances representing the provided list of OrderMaster models.</returns>
        public static List<OrderViewModel> MapGetOrder(this List<OrderMaster> orderMasterList)
        {
            return orderMasterList.Select(x => x.GetOrderDetails()).ToList();
        }

        /// <summary>
            /// Converts an OrderViewModel instance to an OrderMaster model.
            /// </summary>
            /// <param name="orderViewModel">The OrderViewModel instance to map.</param>
            /// <returns>An OrderMaster model representing the provided OrderViewModel instance.</returns>
        public static OrderMaster MapCreateOrder(this OrderViewModel orderViewModel)
        {
            return new OrderMaster
            {
                Id = orderViewModel.Id,
                VendorId = orderViewModel.VendorId,
                OrderTypeId = orderViewModel.OrderTypeId,
                OrderName = orderViewModel.OrderName,
                OrderNumber = orderViewModel.OrderNumber,
                OrderDate = orderViewModel.OrderDate,
                ShipmentDetails = orderViewModel.ShipmentDetails,
                IsCancelled = orderViewModel.IsCancelled,
                IsActive = orderViewModel.IsActive,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,

            };
        }
    }
}

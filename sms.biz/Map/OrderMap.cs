using Microsoft.EntityFrameworkCore;
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
    public static class OrderMap
    {
        /// <summary>
            /// Converts an OrderMaster model to an OrderViewModel instance.
            /// </summary>
            /// <param name="orderMaster">The OrderMaster model to map.</param>
            /// <returns>An OrderViewModel instance representing the provided OrderMaster model.</returns>
        public static OrderViewModel GetOrderDetails(this OrderMaster orderMaster, ApplicationDbContext context)
        {
            var query = from om in context.OrderMaster
                        join vm in context.VendorMaster on om.VendorId equals vm.Id
                        join ot in context.OrderTypeMaster on om.OrderTypeId equals ot.Id
                        join oi in context.OrderItmeMaster on om.Id equals oi.OrderMasterId // Join with OrderItmeMaster
                        where om.Id == orderMaster.Id
                        select new OrderViewModel
                        {
                            Id = orderMaster.Id,
                            VendorId = orderMaster.VendorId,
                            Name = vm.Name,
                            OrderTypeId = orderMaster.OrderTypeId,
                            OrderName = orderMaster.OrderName,
                            OrderNumber = orderMaster.OrderNumber,
                            OrderDate = orderMaster.OrderDate,
                            ShipmentDetails = orderMaster.ShipmentDetails,
                            IsCancelled = orderMaster.IsCancelled,
                            IsActive = orderMaster.IsActive,
                            IsSubmitted = orderMaster.IsSubmitted,
                            CreatedBy = orderMaster.CreatedBy,
                            CreatedDate = orderMaster.CreatedDate,
                            ModifiedDate = orderMaster.ModifiedDate,
                            ModifiedBy = orderMaster.ModifiedBy,
                            OrderTypeName = ot.OrderTypeName,

                            // Include OrderItems in the OrderViewModel
                            OrderItems = context.OrderItmeMaster
                                .Where(oi => oi.OrderMasterId == orderMaster.Id)
                                .Select(oi => new OrderItemViewModel
                                {
                                    // Map OrderItem properties here
                                    ItemID = oi.ItemID,
                                    Quantity = oi.Quantity,
                                    // Include other properties as needed
                                })
                                .ToList(),
                        };

            return query.FirstOrDefault();
        }

        /// <summary>
            /// Converts a list of OrderMaster models to a list of OrderViewModel instances.
            /// </summary>
            /// <param name="orderMasterList">The list of OrderMaster models to map.</param>
            /// <returns>A list of OrderViewModel instances representing the provided list of OrderMaster models.</returns>
        public static List<OrderViewModel> MapGetOrder(this List<OrderMaster> orderMasterList, ApplicationDbContext context)
        {
            return orderMasterList.Select(x => x.GetOrderDetails(context)).ToList();
        }

        /// <summary>
            /// Converts an OrderViewModel instance to an OrderMaster model.
            /// </summary>
            /// <param name="orderViewModel">The OrderViewModel instance to map.</param>
            /// <returns>An OrderMaster model representing the provided OrderViewModel instance.</returns>
        public static OrderMaster MapCreateOrder(this OrderViewModel orderViewModel, List<OrderItemViewModel> orderItems, ApplicationDbContext dbContext)
        {
            var orderMaster = new OrderMaster
            {
               
                VendorId = orderViewModel.VendorId,
                OrderTypeId = orderViewModel.OrderTypeId,
                OrderName = orderViewModel.OrderName,
                OrderNumber = orderViewModel.OrderNumber,
                OrderDate = orderViewModel.OrderDate,
                ShipmentDetails = orderViewModel.ShipmentDetails,
                IsCancelled = orderViewModel.IsCancelled,
                IsActive = true,
                IsSubmitted=orderViewModel.IsSubmitted,
           
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
            };

            var savedOrderMaster = dbContext.OrderMaster.Add(orderMaster);
            dbContext.SaveChanges();
            int newOrderMasterId = savedOrderMaster.Entity.Id;

            var OrderItmeMasters = orderItems.Select(item => new OrderItmeMaster
            {
                OrderMasterId= newOrderMasterId, // Use the obtained ID
                ItemID = item.ItemID,
                Quantity = item.Quantity,
               
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now,
            }).ToList();

            // Assign the mapped purchaseItemMasters to purchaseMaster
            orderMaster.OrderItmeMasters = OrderItmeMasters;

            return orderMaster;
        }
    }
}
        
    


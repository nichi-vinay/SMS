﻿using Microsoft.EntityFrameworkCore;
using sms.biz.Map;
using sms.data;
using sms.data.Models;
using sms.viewmodels;
using System.Collections.Generic;
using System.Linq;

namespace sms.biz.Logic
{
    public class OrderLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<OrderViewModel> GetAllOrders()
        {
            return OrderMap.MapGetOrder(_applicationDbContext.OrderMaster.Where(x => x.IsActive == true).ToList(), _applicationDbContext);
        }

        public OrderViewModel GetOrder(int id)
        {
            var data = OrderMap.GetOrderDetails(_applicationDbContext.OrderMaster.FirstOrDefault(x => x.Id == id), _applicationDbContext);
            return data;
        }

        public int AddOrder(OrderViewModel orderViewModel, List<OrderItemViewModel> orderItems)
        {
            

            var orderMaster = OrderMap.MapCreateOrder(orderViewModel, orderItems, _applicationDbContext);

            _applicationDbContext.SaveChanges();

            return orderMaster.Id;
        }

        public bool DeleteOrder(int id)
        {
            var order = _applicationDbContext.OrderMaster.FirstOrDefault(x => x.Id == id);

            if (order != null)
            {
                order.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UpdateOrder(OrderViewModel order)
        {
            OrderMaster orderMaster = new OrderMaster();
            using (var dbContext = _applicationDbContext)
            {
                orderMaster = dbContext.OrderMaster.FirstOrDefault(x => x.Id == order.Id);
                orderMaster.VendorId = order.VendorId;
                orderMaster.OrderTypeId = order.OrderTypeId;
                orderMaster.OrderName = order.OrderName;
                orderMaster.OrderNumber = order.OrderNumber;
                orderMaster.OrderDate = order.OrderDate;
                orderMaster.ShipmentDetails = order.ShipmentDetails;
                orderMaster.IsCancelled = order.IsCancelled;
                orderMaster.IsActive = true;
                orderMaster.IsSubmitted = orderMaster.IsSubmitted;
               


                dbContext.SaveChanges();
            }
            return true;
        }
    }
}


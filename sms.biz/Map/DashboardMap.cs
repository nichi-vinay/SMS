using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class DashboardMap
    {
        public static DashboardViewModel GetAllDashboard(this DashboardMaster dashboardMaster)
        {
            DashboardViewModel viewModel = new DashboardViewModel
            {
                Id = dashboardMaster.Id,
                Image = dashboardMaster.Image,
                IsAvilable = dashboardMaster.IsAvilable,
                IsActive = dashboardMaster.IsActive,
                CreatedBy = dashboardMaster.CreatedBy,
                CreatedDate = dashboardMaster.CreatedDate
            };
            return viewModel;
        }
        public static List<DashboardViewModel>MapGetDashboard(this List<DashboardMaster> dashboardMasterList)
        {
            return dashboardMasterList.Select(x=>x.GetAllDashboard()).ToList();
        }

        public static DashboardMaster MapCreateDashboardDetails(this DashboardViewModel viewModel)
        {
            return new DashboardMaster
            {
                Id = viewModel.Id,
                Image = viewModel.Image,
                IsAvilable = viewModel.IsAvilable,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
        }
    }
}

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
    public class DashboardLogic
    {
        private readonly ApplicationDbContext _context;

        public DashboardLogic(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public List<DashboardViewModel>GetAllDashboard()
        {
            return DashboardMap.MapGetDashboard(_context.DashboardMaster.Where(x=>x.IsActive==true).ToList());
        }

        public DashboardViewModel GetDashboard(int id)
        {
            return DashboardMap.GetAllDashboard(_context.DashboardMaster.FirstOrDefault(x=>x.Id==id));
        }

        public int AddDashboard(DashboardViewModel dashboard)
        {
            DashboardMaster dashboardMaster = DashboardMap.MapCreateDashboardDetails(dashboard);

            var entityEntry = _context.DashboardMaster.Add(dashboardMaster);
            _context.SaveChanges();

            return entityEntry.Entity.Id;
        }
        public bool DeleteDashBoard(int id)
        {
            var dashboard = _context.DashboardMaster.FirstOrDefault(x => x.Id == id);
            if (dashboard != null)
            {
                dashboard.IsActive = false;
                _context.SaveChanges();
                return true;

            }
            return false;
        }

        public bool UpdateDashboard(DashboardViewModel dashboard)
        {
            using (var context = _context)
            {
                var dashboards =_context.DashboardMaster.FirstOrDefault(x=>x.Id== dashboard.Id);
                if (dashboards != null)
                {
                    dashboards.Image = dashboard.Image;
                    dashboards.IsAvilable=dashboard.IsAvilable;
                    dashboards.IsActive=dashboard.IsActive;
                    _context.SaveChanges();
                    return true;

                }
                return false;
            }
        }
    }
}

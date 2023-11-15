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
    public class ReturnLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReturnLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<ReturnViewModel>GetAllRetunDetails()
        {
            return ReturnMap.MapGetReturn(_applicationDbContext.ReturnMaster.Where(x=>x.IsActive==true).ToList());
        }

        public ReturnViewModel GetReturn(int id)
        {
            return ReturnMap.GetAllReturns(_applicationDbContext.ReturnMaster.FirstOrDefault(x => x.Id == id));
        }

        public int AddReturns(ReturnViewModel model)
        {
            ReturnMaster returnMaster = ReturnMap.MapCreateReturn(model);

            var entityEntry = _applicationDbContext.ReturnMaster.Add(returnMaster);
            _applicationDbContext.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteReturns(int id)
        {
            var returns = _applicationDbContext.ReturnMaster.FirstOrDefault(x => x.Id == id);
            if (returns != null)
            {
                returns.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
                
            }
            return false;
            
        }

        public bool UpdateReturn(ReturnViewModel model)
        {
            using (var context = _applicationDbContext)
            {
                var ReturnMaster = context.ReturnMaster.FirstOrDefault(x=>x.Id == model.Id);

                if(ReturnMaster != null)
                {
                    ReturnMaster.VendorId = model.VendorId;
                    ReturnMaster.InvoiceName = model.InvoiceName;
                    ReturnMaster.ReturnNumber = model.ReturnNumber;
                    ReturnMaster.ReturnDate = model.ReturnDate;
                    ReturnMaster.ShipmentDetails = model.ShipmentDetails;
                    ReturnMaster.TaxNumber = model.TaxNumber;
                    ReturnMaster.IsActive = true;
                    ReturnMaster.IsCanceled= model.IsCanceled;
                    _applicationDbContext.SaveChanges();
                    return true;

                }
                return false;
            }
        }
    }
}

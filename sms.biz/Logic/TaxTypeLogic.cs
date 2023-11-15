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
    public class TaxTypeLogic
    {
        private readonly ApplicationDbContext _context;

        public TaxTypeLogic(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public List<TaxTypeViewModel>GetAllTaxType()
        {
            return TaxTypeMap.MapGetTaxType(_context.TaxTypeMaster.Where(x=>x.IsActive==true).ToList());
        }

        public TaxTypeViewModel GetTaxType(int id)
        {
            return TaxTypeMap.GetAllTaxtype(_context.TaxTypeMaster.FirstOrDefault(x=>x.Id==id));
        }

        public int AddTaxType(TaxTypeViewModel item)
        {
            TaxTypeMaster salesMaster = TaxTypeMap.MapCreateTaxType(item);

            var entityEntry = _context.TaxTypeMaster.Add(salesMaster);
            _context.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteTaxType(int id)
        {
            var taxtype = _context.TaxTypeMaster.FirstOrDefault(x => x.Id == id);
            if (taxtype != null)
            {
                taxtype.IsActive = false;
                _context.SaveChanges();
                return true;

            }
            return false;
        }

        public bool UpdateTaxType(TaxTypeViewModel item)
        {
            using (var context = _context)
            {
                var TaxType = _context.TaxTypeMaster.FirstOrDefault(x=>x.Id == item.Id);

                if (TaxType != null)
                {
                    TaxType.Name = item.Name;
                    TaxType.IsActive = true;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}

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
    public class UnitTypeLogic
    {
        private readonly ApplicationDbContext _context;

        public UnitTypeLogic(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public List<UnitTypeViewModel>GetUnitTypes()
        {
            return UnitTypeMap.MapUnitType(_context.UnitTypeMaster.Where(x=>x.IsActive==true).ToList());
        }

        public UnitTypeViewModel GetUnitType(int id)
        {
            return UnitTypeMap.GetAllUnitType(_context.UnitTypeMaster.FirstOrDefault(x => x.Id == id));
        }

        public int AddUnitType(UnitTypeViewModel unitType)
        {
            UnitTypeMaster unitTypeMaster = UnitTypeMap.MapCreateUnitType(unitType);

            var entityEntry = _context.UnitTypeMaster.Add(unitTypeMaster);
            _context.SaveChanges();

            return entityEntry.Entity.Id;
        }

        public bool DeleteUnitType(int id)
        {
            var unittype = _context.UnitTypeMaster.FirstOrDefault(x => x.Id == id);
            if (unittype != null)
            {
                unittype.IsActive = false;
                _context.SaveChanges();
                return true;

            }
            return false;
        }

        public bool UpdateUnitType(UnitTypeViewModel unitTypeViewModel)
        {
            using (var context = _context)
            {
                var unittype = _context.UnitTypeMaster.FirstOrDefault(x=>x.Id == unitTypeViewModel.Id);
                if (unittype != null)
                {
                    unittype.UnitTypeName= unitTypeViewModel.UnitTypeName;
                    unittype.IsActive = true;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}

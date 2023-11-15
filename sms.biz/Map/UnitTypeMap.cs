using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class UnitTypeMap
    {
        public static UnitTypeViewModel GetAllUnitType(this UnitTypeMaster unitTypeMaster)
        {
            UnitTypeViewModel viewModel = new UnitTypeViewModel
            {
                Id = unitTypeMaster.Id,
                UnitTypeName = unitTypeMaster.UnitTypeName,
                IsActive = true,
                CreatedBy = unitTypeMaster.CreatedBy,
                CreatedDate = unitTypeMaster.CreatedDate
            };
            return viewModel;
        }

        public static List<UnitTypeViewModel>MapUnitType(this List<UnitTypeMaster> unitTypeMasterList)
        {
            return unitTypeMasterList.Select(x=>x.GetAllUnitType()).ToList();
        }

        public static UnitTypeMaster MapCreateUnitType(this UnitTypeViewModel viewModel)
        {
            return new UnitTypeMaster
            {
                Id = viewModel.Id,
                UnitTypeName = viewModel.UnitTypeName,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now

            };
        }
    }
}

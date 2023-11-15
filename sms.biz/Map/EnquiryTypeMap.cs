using sms.data.Models;
using sms.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.biz.Map
{
    public static class EnquiryTypeMap
    {
        public static EnquiryTypeViewModel GetallEnquiryType(this EnquiryTypeMaster enquiryTypeMaster)
        {
            EnquiryTypeViewModel enquiryTypeViewModel = new EnquiryTypeViewModel()
            {
                Id = enquiryTypeMaster.Id,
                EnquiryTypeName = enquiryTypeMaster.EnquiryTypeName,
                IsActive = enquiryTypeMaster.IsActive
            };
            return enquiryTypeViewModel;
        }

        public static List<EnquiryTypeViewModel> MapGetEnquiryType( this List<EnquiryTypeMaster> enquiryTypeMasterList )
        {
            return enquiryTypeMasterList.Select(x=>x.GetallEnquiryType()).ToList();
        }

        public static EnquiryTypeMaster MapCreateEnquiryType(this EnquiryTypeViewModel enquiryTypeViewModel)
        {
            return new EnquiryTypeMaster 
            { 
                Id= enquiryTypeViewModel.Id,
                EnquiryTypeName= enquiryTypeViewModel.EnquiryTypeName,
                IsActive = enquiryTypeViewModel.IsActive
            
            };
        }
    }
}

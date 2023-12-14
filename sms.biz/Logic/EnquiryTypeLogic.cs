using sms.biz.Map;
using sms.data;
using sms.data.Models;
using sms.viewmodels;


namespace sms.biz.Logic
{
    public class EnquiryTypeLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EnquiryTypeLogic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<EnquiryTypeViewModel> GetAllEnquiry()
        {
            return EnquiryTypeMap.MapGetEnquiryType(_applicationDbContext.EnquiryTypeMaster.Where(x=>x.IsActive).ToList());
        }

        public EnquiryTypeViewModel GetEnquiry(int id)
        {
            return EnquiryTypeMap.GetallEnquiryType(_applicationDbContext.EnquiryTypeMaster.FirstOrDefault(x=>x.Id == id));
        }

        public int AddEnquirytype(EnquiryTypeViewModel model)
        {
            EnquiryTypeMaster enquiryTypeMaster = EnquiryTypeMap.MapCreateEnquiryType(model);
            var EntityEntry = _applicationDbContext.EnquiryTypeMaster.Add(enquiryTypeMaster);
            _applicationDbContext.SaveChanges();
            return EntityEntry.Entity.Id;
        }

        public bool DeleteEnquirytype(int id)
        {
            var Enquiry = _applicationDbContext.EnquiryTypeMaster.FirstOrDefault(x=>x.Id == id);
            if(Enquiry != null)
            {
                Enquiry.IsActive = false;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateEnquiryType(EnquiryTypeViewModel model)
        {
            var Enquiry = _applicationDbContext.EnquiryTypeMaster.FirstOrDefault(x => x.Id == model.Id);
            if(Enquiry != null)
            {
                Enquiry.EnquiryTypeName = model.EnquiryTypeName;
                Enquiry.IsActive = true;
                _applicationDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

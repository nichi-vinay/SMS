//using sms.biz.Map;
//using sms.data;
//using sms.viewmodels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

//namespace sms.biz.Logic
//{
//    public class SalesTransactionLogic
//    {
//        private readonly ApplicationDbContext _applicationDbContext;

//        public SalesTransactionLogic(ApplicationDbContext applicationDbContext)
//        {
//            _applicationDbContext = applicationDbContext;
//        }

//        public List<SalesTransactionViewModel> GetAllSalesTransaction()
//        {
//            return SalesTransactionMap.MapGetSalesTransaction(_applicationDbContext.SalesTransactions.ToList());
//        }

//        public SalesTransactionViewModel GetSalesTransaction(int id)
//        {
//            return SalesTransactionMap.GetAllSalesTransaction(_applicationDbContext.SalesTransactions.FirstOrDefault(x => x.Id == id));
//        }

//    }
//}

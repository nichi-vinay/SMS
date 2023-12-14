//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using sms.data.Models;
//using sms.viewmodels;

//namespace sms.biz.Map
//{
//    public static class SalesTransactionMap
//    {
//        public static SalesTransactionViewModel GetAllSalesTransaction(this SalesTransactionsMaster salesTransactionsMaster)
//        {
//            SalesTransactionViewModel viewModel = new SalesTransactionViewModel()
//            {
//                Id = salesTransactionsMaster.Id,
//                Cards = salesTransactionsMaster.Cards,
//                Cash = salesTransactionsMaster.Cash,
//                Cheque = salesTransactionsMaster.Cheque,
//                Online = salesTransactionsMaster.Online,
//                TotalPaid = salesTransactionsMaster.TotalPaid,
//            };
//            return viewModel;
//        }
//        public static List<SalesTransactionViewModel> MapGetSalesTransaction(this List<SalesTransactionsMaster> salesTransactionsMaster)
//        {
//            return salesTransactionsMaster.Select(x => x.GetAllSalesTransaction()).ToList();
//        }
//    }
//}

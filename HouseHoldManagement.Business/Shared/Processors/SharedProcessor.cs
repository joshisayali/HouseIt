using HouseHoldManagement.Business.Expense;
using HouseHoldManagement.Data;
using HouseHoldManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHoldManagement.Business.Shared
{
    public class SharedProcessor
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        //public FilterResultViewModel GetFilterResultView()
        //{
        //    List<ExpenseType> expenseTypes = unitOfWork.ExpenseTypeRepository.Get(orderBy: q => q.OrderBy(d => d.ExpenseTypeId)).ToList();
        //    List<PaymentMode> paymentModes = unitOfWork.PaymentModeRepository.Get(orderBy: q => q.OrderBy(d => d.PaymentModeId)).ToList();

        //    FilterResultViewModel filterResult = new FilterResultViewModel();
        //    //filterResult.FromDate = new DateTime();
        //    //filterResult.ToDate = new DateTime();
        //    filterResult.ExpenseTypes = ObjectMapper.MapObjects<ExpenseType, ExpenseTypeViewModel>(expenseTypes);
        //    filterResult.PaymentModes = ObjectMapper.MapObjects<PaymentMode, PaymentModeViewModel>(paymentModes);

        //    return filterResult;
        //}

        public List<ExpenseTypeViewModel> GetExpenseTypes()
        {
            List<ExpenseTypeViewModel> expenseTypes = new List<ExpenseTypeViewModel>();
            List<ExpenseType> items = unitOfWork.ExpenseTypeRepository.Get(orderBy: q => q.OrderBy(d => d.ExpenseTypeId)).ToList();
            expenseTypes = ObjectMapper.MapObjects<ExpenseType, ExpenseTypeViewModel>(items);
            return expenseTypes;
        }
        
        public List<PaymentModeViewModel> GetPaymentModes()
        {
            List<PaymentModeViewModel> paymentModes = new List<PaymentModeViewModel>();
            List<PaymentMode> items = unitOfWork.PaymentModeRepository.Get(orderBy: q => q.OrderBy(d => d.PaymentModeId)).ToList();
            paymentModes = ObjectMapper.MapObjects<PaymentMode, PaymentModeViewModel>(items);
            return paymentModes;

        } 
    }
}

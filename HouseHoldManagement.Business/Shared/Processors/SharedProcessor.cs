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

        public List<ExpenseCategoryViewModel> GetExpenseCategories()
        {
            List<ExpenseCategory> items = unitOfWork.ExpenseCategoryRepository.Get(orderBy: q => q.OrderBy(d => d.ExpenseCategoryId)).ToList();
            List<ExpenseCategoryViewModel> expenseCategories = ObjectMapper.MapObjects<ExpenseCategory, ExpenseCategoryViewModel>(items);
            return expenseCategories;
        }

        public List<ExpenseSubCategoryViewModel> GetExpenseSubCategories()
        {
            List<ExpenseSubCategory> items = unitOfWork.ExpenseSubCategoryRepository.Get(orderBy: q => q.OrderBy(d => d.ExpenseSubCategoryId)).ToList();
            List<ExpenseSubCategoryViewModel> expenseSubCategories = ObjectMapper.MapObjects<ExpenseSubCategory, ExpenseSubCategoryViewModel>(items);
            return expenseSubCategories;
        }

        public List<ExpenseRepeatFrequencyViewModel> GetExpenseRepeatFrequency()
        {
            List<ExpenseRepeatFrequency> items = unitOfWork.ExpenseRepeatFrequencyRepository.Get(orderBy: q => q.OrderBy(d => d.RepeatId)).ToList();
            List<ExpenseRepeatFrequencyViewModel> expenseRepeatFrequencies = ObjectMapper.MapObjects<ExpenseRepeatFrequency, ExpenseRepeatFrequencyViewModel>(items);
            return expenseRepeatFrequencies;
        }
    }
}

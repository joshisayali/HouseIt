using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHoldManagement.Data
{
    public class UnitOfWork : IDisposable
    {
        HouseHoldManagementEntities context = new HouseHoldManagementEntities();
        //Repositories for each entity
        GenericRepository<SpentAmount> spentAmountRepository;       
        GenericRepository<EarnedAmount> earnedAmountRepository;
        GenericRepository<ExpenseType> expenseTypeRepository;
        GenericRepository<PaymentMode> paymentModeRepository;
        GenericRepository<ExpenseCategory> expenseCategoryRepository;
        GenericRepository<ExpenseSubCategory> expenseSubCategoryRepository;
        GenericRepository<ExpenseRepeatFrequency> expenseRepeatFrequencyRepository;

        public GenericRepository<SpentAmount> SpentAmountRepository
        {
            get
            {
                if (spentAmountRepository == null)
                {
                    spentAmountRepository = new GenericRepository<SpentAmount>(context);
                }
                return spentAmountRepository;
            }
        }        
        public GenericRepository<EarnedAmount> EarnedAmountRepository
        {
            get
            {
                if (earnedAmountRepository == null)
                {
                    earnedAmountRepository = new GenericRepository<EarnedAmount>(context);
                }
                return earnedAmountRepository;
            }
        }
        public GenericRepository<ExpenseType> ExpenseTypeRepository
        {
            get
            {
                if (expenseTypeRepository == null)
                {
                    expenseTypeRepository = new GenericRepository<ExpenseType>(context);
                }
                return expenseTypeRepository;
            }
        }

        public GenericRepository<ExpenseCategory> ExpenseCategoryRepository
        {
            get
            {
                if (expenseCategoryRepository == null)
                {
                    expenseCategoryRepository = new GenericRepository<ExpenseCategory>(context);
                }
                return expenseCategoryRepository;
            }
        }

        public GenericRepository<ExpenseSubCategory> ExpenseSubCategoryRepository
        {
            get
            {
                if(expenseSubCategoryRepository == null)
                {
                    expenseSubCategoryRepository = new GenericRepository<ExpenseSubCategory>(context);
                }
                return expenseSubCategoryRepository;
            }
        }

        public GenericRepository<ExpenseRepeatFrequency> ExpenseRepeatFrequencyRepository
        {
            get
            {
                if(expenseRepeatFrequencyRepository == null)
                {
                    expenseRepeatFrequencyRepository = new GenericRepository<ExpenseRepeatFrequency>(context);
                }
                return expenseRepeatFrequencyRepository;
            }
        }


        public GenericRepository<PaymentMode> PaymentModeRepository
        {
            get
            {
                if (paymentModeRepository == null)
                {
                    paymentModeRepository = new GenericRepository<PaymentMode>(context);
                }
                return paymentModeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (true)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

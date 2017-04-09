using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HouseHoldManagement.Business.Shared;

namespace HouseHoldManagement.Business.Expense
{
    public class GetSpentAmountViewModel
    {
        public int SpentAmountID { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime SpentAmountDate { get; set; }
        [Display(Name = "Item")]
        public string SpentAmountItem { get; set; }
        [Display(Name = "Spent")]
        public int AmountSpent { get; set; }
        [Display(Name = "Expense Type")]
        public ExpenseTypeViewModel ExpenseType { get; set; }
        [Display(Name = "Payment Mode")]
        public PaymentModeViewModel PaymentMode { get; set; }
        [Display(Name = "Details")]
        public string SpentAmountDetails { get; set; }
        [Display(Name = "Is Recurring")]
        public bool IsRecurringExpense { get; set; }
        [Display(Name = "Category")]
        public ExpenseCategoryViewModel ExpenseCategory { get; set; }
        [Display(Name = "Category")]
        [Required]
        public ExpenseSubCategoryViewModel ExpenseSubCategory { get; set; }
        [Display(Name = "Repeat")]
        [Required]
        public ExpenseRepeatFrequencyViewModel ExpenseRepeatFrequency { get; set; }
    }
}

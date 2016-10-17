using HouseHoldManagement.Business.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHoldManagement.Business.Expense
{
    public class CreateSpentAmountViewModel
    {
        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date)]        
        public DateTime SpentAmountDate { get; set; }
        [Display(Name = "Item")]
        [Required]
        public string SpentAmountItem { get; set; }
        [Display(Name = "Spent")]
        [Required]
        [DataType(DataType.Currency)]
        public int AmountSpent { get; set; }
        [Display(Name = "Expense Type")]
        [Required]
        public ExpenseTypeViewModel ExpenseType { get; set; }
        [Display(Name = "Payment Mode")]
        [Required]
        public PaymentModeViewModel PaymentMode { get; set; }
        [Display(Name = "Details")]
        public string SpentAmountDetails { get; set; }
        [Display(Name = "Is Recurring")]
        public bool IsRecurringExpense { get; set; }
    }
}

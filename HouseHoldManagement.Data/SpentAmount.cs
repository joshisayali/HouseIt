//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HouseHoldManagement.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SpentAmount
    {
        public int SpentAmountId { get; set; }
        public System.DateTime SpentAmountDate { get; set; }
        public string SpentAmountItem { get; set; }
        public int AmountSpent { get; set; }
        public int ExpenseTypeID { get; set; }
        public int PaymentModeID { get; set; }
        public string SpentAmountDetails { get; set; }
        public Nullable<bool> IsRecurringExpense { get; set; }
        public Nullable<int> ExpenseSubCategoryId { get; set; }
        public Nullable<int> RepeatId { get; set; }
    
        public virtual ExpenseType ExpenseType { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public virtual ExpenseRepeatFrequency ExpenseRepeatFrequency { get; set; }
        public virtual ExpenseSubCategory ExpenseSubCategory { get; set; }
    }
}

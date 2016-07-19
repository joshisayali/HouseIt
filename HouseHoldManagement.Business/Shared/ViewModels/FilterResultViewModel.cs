using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HouseHoldManagement.Utilities;
using HouseHoldManagement.Business.Expense;

namespace HouseHoldManagement.Business.Shared
{
    public class FilterResultViewModel //: IValidatableObject
    {
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]   
        [ValidateMinMaxDate]        
        public DateTime? FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [ValidateMinMaxDate]   
        [DateCompare("FromDate", CompareToOperation.GreaterThan)]     
        public DateTime? ToDate { get; set; }

        [Display(Name = "Expense Type")]
        public List<ExpenseTypeViewModel> ExpenseTypes { get; set; }

        [Display(Name = "Payment Mode")]
        public List<PaymentModeViewModel> PaymentModes { get; set; }

        public int ExpenseTypeId { get; set; }

        public int PaymentModeId { get; set; }

        public bool IsNullorEmpty
        {
            get
            {
                return ((FromDate == null || (FromDate != null && FromDate == ApplicationDateTime.DefaultDateTime && FromDate == ApplicationDateTime.MinDate))
                        && (ToDate == null || (ToDate != null && ToDate == ApplicationDateTime.DefaultDateTime && FromDate == ApplicationDateTime.MinDate))
                        && ExpenseTypeId == 0
                        && PaymentModeId == 0);
            }
        }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(FromDate != null && FromDate.Value.Year == ApplicationDateTime.DefaultDateTime.Year)
        //    {
        //        yield return new ValidationResult("Please enter valid year.", new[] { "FromDate" });
        //    }
        //    if(ToDate != null && ToDate < FromDate)
        //    {
        //        yield return new ValidationResult("Date cannot be lesser than From Date.",new[] { "ToDate" });
        //    }
        //}

    }
}

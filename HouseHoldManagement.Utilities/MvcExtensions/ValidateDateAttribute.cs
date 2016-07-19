using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HouseHoldManagement.Utilities
{
    public class ValidateMinMaxDateAttribute: ValidationAttribute
    {
        //DateTime? _fromDate;
        //DateTime? _toDate;
        //public ValidateDateAttribute(DateTime fromDate, DateTime toDate)
        //{
        //    _fromDate = fromDate;
        //    _toDate = toDate;
        //}
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                bool error = false;
                string errorMessage = string.Empty;
                DateTime item = new DateTime();

                //check if value is DateTime
                if (DateTime.TryParse(value.ToString(),out item))
                {
                    
                    if(item.Year == ApplicationDateTime.DefaultDateTime.Year || item == ApplicationDateTime.MinDate || item == ApplicationDateTime.MaxDate) 
                    {
                        error = true;
                        errorMessage = "Please enter valid date.";
                    }             
                }
                else
                {
                    error = false;
                }  
                
                if(error)
                {
                    return new ValidationResult(errorMessage);
                }              

                //return base.IsValid(value, validationContext);
            }
            return ValidationResult.Success;

        }
    }
}

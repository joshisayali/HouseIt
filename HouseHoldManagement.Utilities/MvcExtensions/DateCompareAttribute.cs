using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HouseHoldManagement.Utilities
{
    public class DateCompareAttribute : ValidationAttribute
    {
        private string _propertyToMatch;
        private CompareToOperation _compareToOperation;
        private DateTime dateToCompare;
        private DateTime dateToMatch;

        public DateCompareAttribute(string propertyToMatch, CompareToOperation compareToOperation)
        {
            _propertyToMatch = propertyToMatch;
            _compareToOperation = compareToOperation;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool error = false;
            string errorMessage = string.Empty;
            Type type = validationContext.ObjectType;
            if (!string.IsNullOrEmpty(_propertyToMatch))
            {
                PropertyInfo property = type.GetProperty(_propertyToMatch);
                var valueToMatch = property.GetValue(validationContext.ObjectInstance);

                if (valueToMatch != null)
                {
                    if (value != null)
                    {
                        if (DateTime.TryParse(value.ToString(), out dateToCompare))
                        {
                            if (DateTime.TryParse(valueToMatch.ToString(), out dateToMatch))
                            {
                                switch (_compareToOperation)
                                {
                                    case (CompareToOperation.EqualTo):
                                        if (dateToCompare != dateToMatch)
                                        {
                                            error = false;
                                            errorMessage = string.Format("{0} is not same as {1}.", validationContext.DisplayName, property.Name);
                                        }
                                        break;
                                    case (CompareToOperation.GreaterThan):
                                        if (dateToCompare < dateToMatch)
                                        {
                                            error = true;
                                            errorMessage = string.Format("{0} must be later than {1}.", validationContext.DisplayName, property.Name);
                                        }
                                        break;
                                    case (CompareToOperation.LessThan):
                                        if (dateToCompare > dateToMatch)
                                        {
                                            error = true;
                                            errorMessage = string.Format("{0} must be earlier than {1}.", validationContext.DisplayName, property.Name);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                throw new InvalidOperationException(string.Format("{0} is not a valid date.", property.Name));
                            }
                        }
                        else
                        {
                            error = true;
                            errorMessage = "Invalid date.";
                        }

                        if (error)
                        {
                            return new ValidationResult(errorMessage);
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}

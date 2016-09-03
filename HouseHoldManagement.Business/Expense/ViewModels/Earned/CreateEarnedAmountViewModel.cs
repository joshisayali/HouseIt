using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HouseHoldManagement.Business.Expense
{
    public class CreateEarnedAmountViewModel
    {
        [Display(Name = "Date", Order = 1)]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EarnedAmountDate { get; set; }

        [Display(Name = "Source", Order = 2)]
        [Required]
        [StringLength(100)]
        public string EarnedAmountSource { get; set; }

        [Display(Name = "Amount", Order = 3)]
        [Required]
        [DataType(DataType.Currency)]
        public int AmountEarned { get; set; }

        [Display(Name = "Details")]
        public string EarnedAmountDetails { get; set; }
    }
}

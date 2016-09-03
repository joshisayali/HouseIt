using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HouseHoldManagement.Business.Expense
{
    public class GetEarnedAmountViewModel
    {
        public int EarnedAmountId { get; set; }

        [Display(Name = "Date",Order = 1)]
        [DataType(DataType.Date)]
        public DateTime EarnedAmountDate { get; set; }

        [Display(Name ="Source",Order = 2)]
        public string EarnedAmountSource { get; set; }

        [Display(Name ="Amount",Order = 3)]
        public int AmountEarned { get; set; }

        [Display(Name = "Details")]
        public string EarnedAmountDetails { get; set; }
    }
}

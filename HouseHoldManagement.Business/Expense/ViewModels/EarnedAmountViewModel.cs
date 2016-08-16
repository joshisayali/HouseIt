using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHoldManagement.Business.Expense
{
    public class EarnedAmountViewModel
    {
        public IList<GetEarnedAmountViewModel> GetEarnedAmount { get; set; }
        public CreateEarnedAmountViewModel CreateEarnedAmount { get; set; }
    }
}

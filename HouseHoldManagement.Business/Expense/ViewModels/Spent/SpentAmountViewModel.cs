using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HouseHoldManagement.Business.Shared;
using PagedList;

namespace HouseHoldManagement.Business.Expense
{
    public class SpentAmountViewModel
    {        
        public IPagedList<GetSpentAmountViewModel> GetSpentAmount { get; set; }
        public CreateSpentAmountViewModel CreateSpentAmount { get; set; }
        public ExpenseFilterViewModel Filter { get; set; }
        
    }
}

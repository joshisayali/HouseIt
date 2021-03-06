﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using HouseHoldManagement.Business.Shared;

namespace HouseHoldManagement.Business.Expense
{
    public class EarnedAmountViewModel
    {
        public IPagedList<GetEarnedAmountViewModel> GetEarnedAmount { get; set; }
        public CreateEarnedAmountViewModel CreateEarnedAmount { get; set; }
        public ExpenseFilterViewModel Filter { get; set; }
    }
}

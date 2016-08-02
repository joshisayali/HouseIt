﻿using HouseHoldManagement.Business.Expense;
using HouseHoldManagement.Business.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseHoldManagement.Controllers
{
    /// <summary>
    /// Class to contain reusable code
    /// </summary>
    public class SharedController : Controller
    {
        //[ChildActionOnly]
        public ActionResult GetFilterView(string postAction, string postController, FilterResultViewModel filterResult)
        {
            //ModealState.IsValid could be added here, but it just duplicates the code
            ModelState.Remove("ExpenseTypeId");
            ModelState.Remove("PaymentModeId");
            SharedProcessor sharedProcessor = new SharedProcessor();
            filterResult.ExpenseTypes = sharedProcessor.GetExpenseTypes();
            filterResult.PaymentModes = sharedProcessor.GetPaymentModes();
            ViewBag.PostAction = postAction;
            ViewBag.PostController = postController;
            return PartialView("_FilterResult", filterResult);

        }


    }
}
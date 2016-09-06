using HouseHoldManagement.Business.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HouseHoldManagement.Business.Shared;
using HouseHoldManagement.Utilities;

namespace HouseHoldManagement.Controllers
{
    public class EarnedController : Controller
    {
        // GET: Earned
        //Parameter currentFilter removed - cannot pass complex type from view using route data
        //New parameter month introduced - 
        //      1. This was added to avoid adding new parameters "FromDate" and "ToDate" and make use of existing parameter "filter"
        //      2. This was added to avoid any date calculations on client side and send it as route values by ajax call
        public ActionResult EarnedAmount(string sortOrder, int? pageNumber, string month, ExpenseFilterViewModel filter)
        {
            //Code for paging
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentPageNumber = pageNumber;
            //Means new filter conditions added. currentFilter parameter is required to maintain previous filter values.
            //With each Get request (sort click, page click) parameter filter becomes null as it is populated only  
            //on POST request that is when Filter button is clicked
            if (!filter.IsNullorEmpty)
            {
                pageNumber = 1;
            }
            else if (month == "current")
            {
                filter = new ExpenseFilterViewModel()
                {
                    FromDate = ApplicationDateTime.FirstDayOfMonth,
                    ToDate = ApplicationDateTime.LastDayOfMonth
                };
            }
            else if (month == "previous")
            {
                filter = new ExpenseFilterViewModel()
                {
                    FromDate = ApplicationDateTime.FirstDayOfMonth.AddMonths(-1),
                    ToDate = ApplicationDateTime.LastDayOfMonth.AddMonths(-1)
                };
            }
            else if (month == "all")
            {
                filter = new ExpenseFilterViewModel
                {
                    FromDate = ApplicationDateTime.MinDate,
                    ToDate = ApplicationDateTime.MaxDate
                };
            }
            else if (HttpContext.Session["CurrentFilter"] == null)
            {
                filter = new ExpenseFilterViewModel();
            }
            else
            {
                filter = (ExpenseFilterViewModel)HttpContext.Session["CurrentFilter"];
            }

            SharedProcessor sharedProcessor = new SharedProcessor();
            filter.ExpenseTypes = sharedProcessor.GetExpenseTypes();
            filter.PaymentModes = sharedProcessor.GetPaymentModes();

            ViewBag.CurrentFilter = filter;
            HttpContext.Session["CurrentFilter"] = filter;


            //Sort Order : Default column
            ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "Date_Desc" : string.Empty;
            //Sort Order: other columns
            ViewBag.SourceSortParam = sortOrder == "Source" ? "Source_Desc" : "Source";
            ViewBag.AmountSortParam = sortOrder == "Amount" ? "Amount_Desc" : "Amount";


            EarnedAmountProcessor earnedAmountProcessor = new EarnedAmountProcessor();
            EarnedAmountViewModel earned = new EarnedAmountViewModel
            {
                GetEarnedAmount = earnedAmountProcessor.GetEarnedAmount(sortOrder, filter).ToPagedList(pageNumber ?? 1, 2),
                CreateEarnedAmount = new CreateEarnedAmountViewModel(),
                Filter = filter
            };

            return View(earned);
        }

        [HttpPost]
        public ActionResult CreateEarnedAmount(EarnedAmountViewModel earned)
        {
            if (ModelState.IsValid)
            {
                CreateEarnedAmountViewModel earnedAmount = earned.CreateEarnedAmount;
                EarnedAmountProcessor earnedAmountProcessor = new EarnedAmountProcessor();
                earnedAmountProcessor.CreateEarnedAmount(earnedAmount);
                return RedirectToAction("EarnedAmount");
            }
            else
            {
                return View(earned);
            }
        }

        //[HttpPost]
        public ActionResult DeleteEarnedAmount(int id)
        {
            EarnedAmountProcessor earnedAmountProcessor = new EarnedAmountProcessor();
            earnedAmountProcessor.DeleteEarnedAmount(id);
            return RedirectToAction("EarnedAmount");
        }

        public ActionResult UpdateEarnedAmount(GetEarnedAmountViewModel earnedAmount)
        {
            if (ModelState.IsValid)
            {
                EarnedAmountProcessor earnedAmountProcessor = new EarnedAmountProcessor();
                GetEarnedAmountViewModel updatedEarnedAmount = earnedAmountProcessor.UpdateEarnedAmount(earnedAmount);
                return Json(updatedEarnedAmount);
            }
            else
            {
                return Json(earnedAmount);
            }

        }

        //public ActionResult EarnedAmountByDate()
        //{
        //    FilterResultViewModel filterResult = new FilterResultViewModel()
        //    {
        //        FromDate = ApplicationDateTime.FirstDayOfMonth,
        //        ToDate = ApplicationDateTime.LastDayOfMonth
        //    };

        //    return RedirectToAction("EarnedAmount", new { sortOrder = "", pageNumber = 1, filter = filterResult });

        //}
    }
}
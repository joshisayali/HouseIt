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
    public class SpentController : Controller
    {
        // GET: Spent
        public ActionResult SpentAmount(string sortOrder, int? page, string month, ExpenseFilterViewModel filter)
        {
            //string url = Url.Action("SpentAmount", "Spent");
            ModelState.Remove("ExpenseTypeId");
            ModelState.Remove("PaymentModeId");
            if (ModelState.IsValid && !filter.IsNullorEmpty)
            {
                page = 1;
            }
            else if(month == "current")
            {
                filter = new ExpenseFilterViewModel()
                {
                    FromDate = ApplicationDateTime.FirstDayOfMonth,
                    ToDate = ApplicationDateTime.LastDayOfMonth
                };

            }
            else if(month == "previous")
            {
                filter = new ExpenseFilterViewModel()
                {
                    FromDate = ApplicationDateTime.FirstDayOfMonth.AddMonths(-1),
                    ToDate = ApplicationDateTime.LastDayOfMonth.AddMonths(-1)
                };
            }
            else if(month == "all")
            {
                filter = new ExpenseFilterViewModel {
                     FromDate = ApplicationDateTime.MinDate,
                     ToDate = ApplicationDateTime.MaxDate
                };
            }
            else if(HttpContext.Session["CurrentFilter"] == null)
            {
                filter = new ExpenseFilterViewModel();                                 
            }
            else
            {
                filter = (ExpenseFilterViewModel)HttpContext.Session["CurrentFilter"];
            }

            HttpContext.Session["CurrentFilter"] = filter;
            //ViewBag.CurrentFilter = filter;

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentPageNumber = page;

            ViewBag.DateSortOrder = sortOrder == "date" ? "date_dec" : "date";
            ViewBag.AmountSortOrder = sortOrder == "amount" ? "amount_desc" : "amount";


            //This is for in-place edit
            SharedProcessor sharedProcessor = new SharedProcessor();
            //ViewBag.ExpenseTypes = sharedProcessor.GetExpenseTypes();
            ViewBag.PaymentModes = sharedProcessor.GetPaymentModes();
            ViewBag.ExpenseCategories = sharedProcessor.GetExpenseCategories();
            ViewBag.ExpenseSubCategories = sharedProcessor.GetExpenseSubCategories();
            ViewBag.ExpenseRepeatFrequencies = sharedProcessor.GetExpenseRepeatFrequency();

            //This code is to populate drop downs in partial view - filter
            filter.ExpenseTypes = sharedProcessor.GetExpenseTypes();
            filter.PaymentModes = sharedProcessor.GetPaymentModes();
            //filter.ExpenseCategory = sharedProcessor.GetExpenseCategories();
            //filter.ExpenseSubCategory = sharedProcessor.GetExpenseSubCategories();
            //filter.ExpenseRepeatFrequency = sharedProcessor.GetExpenseRepeatFrequency();


            SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
            SpentAmountViewModel spent = new SpentAmountViewModel()
            {
                CreateSpentAmount = new CreateSpentAmountViewModel(),
                GetSpentAmount = spentAmountProcessor.GetSpentAmount(sortOrder, filter).ToPagedList(page ?? 1, 10),
                Filter = filter                 
            };            
            
            return View(spent);

        }         
        
        public ActionResult UpdateSpentAmount(GetSpentAmountViewModel spentAmountToUpdate)
        {
            SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
            GetSpentAmountViewModel updatedSpentAmount = spentAmountProcessor.UpdateSpentAmount(spentAmountToUpdate);
            return Json(updatedSpentAmount);
        }

        public ActionResult DeleteSpentAmount(int spentAmountId)
        {
            SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
            spentAmountProcessor.DeleteSpentAmount(spentAmountId);
            return RedirectToAction("SpentAmount");
        }

        [HttpPost]
        public ActionResult CreateSpentAmount(SpentAmountViewModel spent)
        {
            if (ModelState.IsValid)
            {
                CreateSpentAmountViewModel spentAmount = spent.CreateSpentAmount;
                SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
                spentAmountProcessor.CreateSpentAmount(spentAmount);
                return RedirectToAction("SpentAmount");
            }
            else
            {
                return View(spent);
            }            
        }

        //[HttpPost]
        //public ActionResult CreateSpentAmount(CreateSpentAmountViewModel spentAmountToCreate)
        //{      
        //    if(ModelState.IsValid)
        //    {
        //        SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
        //        spentAmountProcessor.CreateSpentAmount(spentAmountToCreate);

        //        SharedProcessor sharedProcessor = new SharedProcessor();
        //        ViewBag.ExpenseTypes = sharedProcessor.GetExpenseTypes();
        //        ViewBag.PaymentModes = sharedProcessor.GetPaymentModes();
        //        //return PartialView("_CreateSpentAmount", new CreateSpentAmountViewModel());
        //        //string url = Url.Action("SpentAmount", "Spent");
                
        //        return Json(new { redirectUrl = "/Spent/SpentAmount", isRedirect = true });
        //    }  
        //    else
        //    {
        //        SharedProcessor sharedProcessor = new SharedProcessor();
        //        ViewBag.ExpenseTypes = sharedProcessor.GetExpenseTypes();
        //        ViewBag.PaymentModes = sharedProcessor.GetPaymentModes();
        //        return PartialView("_CreateSpentAmount", spentAmountToCreate);
        //    }           
        //}

        //[HttpPost]
        //public ActionResult FilterResult(FilterResultViewModel filter)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        List<SpentAmountViewModel> spentAmounts = new List<SpentAmountViewModel>();
        //        SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
        //        //spentAmounts = spentAmountProcessor.GetSpentAmount(filter);
        //        return View("SpentAmount", spentAmounts);
        //    }
        //    else
        //    {
        //        return View("SpentAmount");
        //    }

        //}
    }
}
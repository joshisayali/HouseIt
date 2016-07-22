using HouseHoldManagement.Business.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HouseHoldManagement.Business.Shared;

namespace HouseHoldManagement.Controllers.Expense
{
    public class SpentController : Controller
    {
        // GET: Spent
        public ActionResult SpentAmount(string sortOrder, int? page, FilterResultViewModel filter)
        {
            ModelState.Remove("ExpenseTypeId");
            ModelState.Remove("PaymentModeId");
            if (ModelState.IsValid && !filter.IsNullorEmpty)
            {
                page = 1;
            }
            else
            {
                filter = (FilterResultViewModel)HttpContext.Session["CurrentFilter"];
            }

            HttpContext.Session["CurrentFilter"] = filter;
            //ViewBag.CurrentFilter = filter;

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentPageNumber = page;

            ViewBag.DateSortOrder = sortOrder == "date" ? "date_dec" : "date";
            ViewBag.AmountSortOrder = sortOrder == "amount" ? "amount_desc" : "amount";

            SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
            List<SpentAmountViewModel> spentAmounts = spentAmountProcessor.GetSpentAmount(sortOrder, filter);

            SharedProcessor sharedProcessor = new SharedProcessor();
            ViewBag.ExpenseTypes = sharedProcessor.GetExpenseTypes();
            ViewBag.PaymentModes = sharedProcessor.GetPaymentModes();

            int pageSize = 10;
            return View(spentAmounts.ToPagedList(page ?? 1, pageSize));

        }         
        
        public ActionResult UpdateSpentAmount(SpentAmountViewModel spentAmountToUpdate)
        {
            SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
            SpentAmountViewModel updatedSpentAmount = spentAmountProcessor.UpdateSpentAmount(spentAmountToUpdate);
            return Json(updatedSpentAmount);
        }

        public ActionResult DeleteSpentAmount(int spentAmountId)
        {
            return RedirectToAction("SpentAmount");
        }

        [ChildActionOnly]
        public PartialViewResult CreateSpentAmount()
        {
            CreateSpentAmountViewModel createSpentAmount = new CreateSpentAmountViewModel();
            SharedProcessor sharedProcessor = new SharedProcessor();
            ViewBag.ExpenseTypes = sharedProcessor.GetExpenseTypes();
            ViewBag.PaymentModes = sharedProcessor.GetPaymentModes();
            return PartialView("_CreateSpentAmount", createSpentAmount);
        }

        [HttpPost]
        public ActionResult CreateSpentAmount(CreateSpentAmountViewModel createSpentAmount)
        {      
            if(ModelState.IsValid)
            {
                SpentAmountProcessor spentAmountProcessor = new SpentAmountProcessor();
                spentAmountProcessor.CreateSpentAmount(createSpentAmount);

                SharedProcessor sharedProcessor = new SharedProcessor();
                ViewBag.ExpenseTypes = sharedProcessor.GetExpenseTypes();
                ViewBag.PaymentModes = sharedProcessor.GetPaymentModes();
                return PartialView("_CreateSpentAmount", new CreateSpentAmountViewModel());
            }  
            else
            {
                SharedProcessor sharedProcessor = new SharedProcessor();
                ViewBag.ExpenseTypes = sharedProcessor.GetExpenseTypes();
                ViewBag.PaymentModes = sharedProcessor.GetPaymentModes();
                return PartialView("_CreateSpentAmount", createSpentAmount);
            }           
        }

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
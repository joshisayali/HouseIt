using HouseHoldManagement.Business.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HouseHoldManagement.Controllers
{
    public class EarnedController : Controller
    {
        // GET: Earned
        public ActionResult EarnedAmount(string sortOrder, int? pageNumber)
        {
            //Code for paging
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentPageNumber = pageNumber;

            //Sort Order : Default column
            ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "Date_Desc" : string.Empty;
            //Sort Order: other columns
            ViewBag.SourceSortParam = sortOrder == "Source" ? "Source_Desc" : "Source";
            ViewBag.AmountSortParam = sortOrder == "Amount" ? "Amount_Desc" : "Amount";
            

            EarnedAmountProcessor earnedAmountProcessor = new EarnedAmountProcessor();
            EarnedAmountViewModel earned = new EarnedAmountViewModel();
            earned.GetEarnedAmount = earnedAmountProcessor.GetEarnedAmount(sortOrder).ToPagedList(pageNumber ?? 1, 2);
            earned.CreateEarnedAmount = new CreateEarnedAmountViewModel();
            return View(earned);
        }

        [HttpPost]
        public ActionResult CreateEarnedAmount(EarnedAmountViewModel earned)
        {
            if(ModelState.IsValid)
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
            if(ModelState.IsValid)
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
    }
}
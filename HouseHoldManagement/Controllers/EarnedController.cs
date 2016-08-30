using HouseHoldManagement.Business.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HouseHoldManagement.Business.Shared;

namespace HouseHoldManagement.Controllers
{
    public class EarnedController : Controller
    {
        // GET: Earned
        //Parameter currentFilter removed - cannot pass complex type from view using route data
        public ActionResult EarnedAmount(string sortOrder, int? pageNumber, FilterResultViewModel filter)
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
            else
            {   
                             
                filter = (FilterResultViewModel)HttpContext.Session["CurrentFilter"];
            }
            ViewBag.CurrentFilter = filter;
            HttpContext.Session["CurrentFilter"] = filter;
            

            //Sort Order : Default column
            ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "Date_Desc" : string.Empty;
            //Sort Order: other columns
            ViewBag.SourceSortParam = sortOrder == "Source" ? "Source_Desc" : "Source";
            ViewBag.AmountSortParam = sortOrder == "Amount" ? "Amount_Desc" : "Amount";
            

            EarnedAmountProcessor earnedAmountProcessor = new EarnedAmountProcessor();
            EarnedAmountViewModel earned = new EarnedAmountViewModel();
            earned.GetEarnedAmount = earnedAmountProcessor.GetEarnedAmount(sortOrder, filter).ToPagedList(pageNumber ?? 1, 2);
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
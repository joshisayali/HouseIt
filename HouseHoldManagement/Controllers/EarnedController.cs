using HouseHoldManagement.Business.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseHoldManagement.Controllers
{
    public class EarnedController : Controller
    {
        // GET: Earned
        public ActionResult EarnedAmount()
        {
            EarnedAmountProcessor earnedAmountProcessor = new EarnedAmountProcessor();
            EarnedAmountViewModel earned = new EarnedAmountViewModel();
            earned.GetEarnedAmount = earnedAmountProcessor.GetEarnedAmount();
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
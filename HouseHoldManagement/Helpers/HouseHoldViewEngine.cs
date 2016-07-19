using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseHoldManagement
{
    public class HouseHoldViewEngine: RazorViewEngine
    {
        public HouseHoldViewEngine()
        {
            var ViewLocations = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Expense/{1}/{0}.cshtml"
            };

            this.PartialViewLocationFormats = ViewLocations;
            this.ViewLocationFormats = ViewLocations;
        }
    }
}
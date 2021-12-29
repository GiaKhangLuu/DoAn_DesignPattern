using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;

namespace ShopCayCanh.Library
{
    public class HomeSite : AbstractSite
    {
        public override ActionResult DirectTo()
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var list = list_cat.Where(m => m.status == 1).
                 Where(m => m.parentid == 0)
                 .OrderBy(m => m.orders);

            return View("~/Views/Site/Index.cshtml", list);
        }
    }
}
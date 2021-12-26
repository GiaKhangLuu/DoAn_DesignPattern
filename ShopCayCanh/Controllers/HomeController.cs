using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;
using ShopCayCanh.Library;

namespace ShopCayCanh.Controllers
{
    public class HomeController : Controller, ISiteStrategy
    {
        public ActionResult Go_To_Site(string slug = "", int id = 0)
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var list = list_cat.Where(m => m.status == 1).
                 Where(m => m.parentid == 0)
                 .OrderBy(m => m.orders);

            return View("~/Views/Site/Index.cshtml", list);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;
using ShopCayCanh.Library;

namespace ShopCayCanh.Controllers
{
    public class CategoryController : Controller, ISiteStrategy
    {
        public ActionResult Go_To_Site(string slug, int id)
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var catid = list_cat.Where(m => m.slug == slug && m.ID == id).First();
            return View("category", catid);
        }
    }
}
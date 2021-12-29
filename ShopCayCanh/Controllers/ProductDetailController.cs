using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;
using ShopCayCanh.Library;

namespace ShopCayCanh.Controllers
{
    public class ProductDetailController : Controller, ISiteStrategy
    {
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        public ActionResult Go_To_Site(string slug, int id)
        {
            var list = db.Products.Where(m => m.status == 1 && m.slug == slug && m.ID == id).First();
            return View("ProductDetail", list);
        }
    }
}
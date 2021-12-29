using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;
using ShopCayCanh.Library;

namespace ShopCayCanh.Controllers
{
    public class PostDetailController : Controller, ISiteStrategy
    {
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        public ActionResult Go_To_Site(string slug, int id)
        {
            var detail = db.posts.Where(m => m.status == 1 && m.slug == slug && m.ID == id).First();
            return View("post_detail", detail);
        }
    }
}
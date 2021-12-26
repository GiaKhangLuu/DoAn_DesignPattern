using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;
using ShopCayCanh.Library;

namespace ShopCayCanh.Controllers
{
    public class PostCategoryController : Controller, ISiteStrategy
    {
        public ActionResult Go_To_Site(string slug, int id)
        {
            var list_topic = Singleton_Topic.GetInstance.list_topic;
            var topic = list_topic.Where(m => m.status == 1 && m.slug == slug && m.ID == id).First();
            return View("post_category", topic);
        }
    }
}
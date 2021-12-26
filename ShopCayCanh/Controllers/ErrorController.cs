using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;
using ShopCayCanh.Library;

namespace ShopCayCanh.Controllers
{
    public class ErrorController : Controller, ISiteStrategy
    {
        public ActionResult Go_To_Site(string slug, int id)
        {
            return View("");
        }
    }
}
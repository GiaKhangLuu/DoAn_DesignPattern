using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCayCanh.Library
{
    public class ErrorSite : AbstractSite
    {
        public override ActionResult DirectTo()
        {
            return View("");
        }
    }
}
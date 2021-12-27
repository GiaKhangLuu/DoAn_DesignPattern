using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;

namespace ShopCayCanh.Controllers
{
    /* 
    ===================================================================================
                        TEMPLATE METHOD PATTERN DEFINITION
    ===================================================================================
    */
    public abstract class AuthTemplateMethodController : Controller
    {
        protected abstract Muser CheckLogin(FormCollection fc);
        protected abstract void SaveSession(Muser muser = null);
        protected abstract ActionResult GoToSite();

        // Template Method
        protected ActionResult Login(FormCollection fc)
        {
            var muser = CheckLogin(fc);
            SaveSession(muser);
            return GoToSite(); 
        }
    }
}
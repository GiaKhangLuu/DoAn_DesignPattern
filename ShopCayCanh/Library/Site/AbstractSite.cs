using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopCayCanh.Models;
using System.Web.Mvc;

namespace ShopCayCanh.Library
{
    public enum SiteName
    {
        Category,
        Error,
        Home,
        Topic,
        PostDetail,
        ProductDetail
    }

    public abstract class AbstractSite : Controller
    {
        protected ShopCayCanhDbContext db = new ShopCayCanhDbContext();
        protected string slug_name;
        protected int slug_id;

        public AbstractSite(string slug_name, int slug_id)
        {
            this.slug_name = slug_name;
            this.slug_id = slug_id;
        }

        public AbstractSite() { }

        public abstract ActionResult DirectTo();

    }
}
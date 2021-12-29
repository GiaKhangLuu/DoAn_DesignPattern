using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;

namespace ShopCayCanh.Library
{
    public class CategorySite : AbstractSite
    {
        public CategorySite(string slug_name, int slug_id) : base(slug_name, slug_id) { }
        
        public override ActionResult DirectTo()
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var catid = list_cat.Where(m => m.slug == slug_name && m.ID == slug_id).First();
            return View("category", catid);
        }
    }
}
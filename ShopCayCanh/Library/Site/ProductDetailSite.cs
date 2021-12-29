using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCayCanh.Library
{
    public class ProductDetailSite : AbstractSite
    {
        public ProductDetailSite(string slug_name, int slug_id) : base(slug_name, slug_id) { }

        public override ActionResult DirectTo()
        {
            var list = db.Products.Where(m => m.status == 1 && m.slug == slug_name && m.ID == slug_id).First();
            return View("ProductDetail", list);
        }
    }
}
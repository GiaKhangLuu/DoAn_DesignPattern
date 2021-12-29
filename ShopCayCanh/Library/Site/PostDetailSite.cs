using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCayCanh.Library
{
    public class PostDetailSite : AbstractSite
    {
        public PostDetailSite(string slug_name, int slug_id) : base(slug_name, slug_id) { }

        public override ActionResult DirectTo()
        {
            var detail = db.posts.Where(m => m.status == 1 && m.slug == slug_name && m.ID == slug_id).First();
            return View("post_detail", detail);
        }
    }
}
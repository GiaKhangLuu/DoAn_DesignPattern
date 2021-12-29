using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Models;

namespace ShopCayCanh.Library
{
    public class TopicSite : AbstractSite
    {
        public TopicSite(string slug_name, int slug_id) : base(slug_name, slug_id) { }

        public override ActionResult DirectTo()
        {
            var list_topic = Singleton_Topic.GetInstance.list_topic;
            var topic = list_topic.Where(m => m.status == 1 && m.slug == slug_name && m.ID == slug_id).First();
            return View("post_category", topic);
        }
    }
}
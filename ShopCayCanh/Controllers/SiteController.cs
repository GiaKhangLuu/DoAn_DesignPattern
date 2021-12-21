﻿using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCayCanh.Controllers
{
    public class SiteController : Controller
    {
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();
        // GET: Site
        public ActionResult Index(int id, String slug = "")
        {
            int page = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = int.Parse(Request.QueryString["page"].ToString());
            }

            if (slug == "")
            {
                return this.home();
            }
            else
            {
                var rowlink = db.Link.Where(m => m.slug == slug);
                if (rowlink.Count() > 0)
                {
                    var link = rowlink.First();
                    if (link.type == "ProductDetail" && link.tableId == 1)
                    {
                        return this.ProductDetail(slug, id);
                    }
                    else if (link.type == "category" && link.tableId == 2)
                    {

                        return this.productOfCategory(slug, id);
                    }
                    else if (link.type == "topic" && link.tableId == 3)
                    {
                        return this.topic_category(slug, id);
                    }
                    else if (link.type == "PostDetail" && link.tableId == 4)
                    {
                        return this.postDetaail(slug);
                    }
                }
                else
                {
                    //slug k co tring ban link
                    return this.page404();
                }
                return this.page404();
            }
          
        }


        public ActionResult home()
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var list = list_cat.Where(m => m.status == 1).
                 Where(m => m.parentid == 0)
                 .OrderBy(m => m.orders);

            return View("Index", list);
        }


        public ActionResult topic_category(String slug, int id)
        {
            var list_topic = Singleton_Topic.GetInstance.list_topic;
            var topic = list_topic.Where(m => m.status == 1 && m.slug == slug && m.ID == id).First();
            return View("post_category", topic);
        }


        private ActionResult ProductDetail(String slug, int id)
        {
            var list = db.Products.Where(m => m.status == 1 && m.slug == slug && m.ID == id).First();
            return View("ProductDetail", list);
        }


        public ActionResult productOfCategory(String slug, int id)
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var catid = list_cat.Where(m => m.slug == slug && m.ID == id).First();

            return View("category", catid);
        }


        public ActionResult postDetaail(String slug)
        {
            var detail = db.posts.Where(m => m.status == 1 && m.slug == slug).First();
            return View("post_detail", detail);
        }


        public ActionResult page404()
        {
            return View("");
        }
    }
}
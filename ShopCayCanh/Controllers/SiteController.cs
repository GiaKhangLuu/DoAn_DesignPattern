using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Library;

namespace ShopCayCanh.Controllers
{
    public class SiteController : Controller
    {
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();
        SiteFactory site_factory = new SiteFactory();
        AbstractSite site;
        
        // GET: Site
        public ActionResult Index(int id = -1, string slug = "")
        {
            int page = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = int.Parse(Request.QueryString["page"].ToString());
            }

            if (slug == "")
            {
                site = site_factory.CreateSite(SiteName.Home, slug, -1);
                return site.DirectTo();
            }
            else
            {
                var rowlink = db.Link.Where(m => m.slug == slug);
                if (rowlink.Count() > 0)
                {
                    var link = rowlink.First();
                    if (link.type == "ProductDetail" && link.tableId == 1)
                    {
                        site = site_factory.CreateSite(SiteName.ProductDetail, slug, id);     
                    }
                    else if (link.type == "category" && link.tableId == 2)
                    {
                        site = site_factory.CreateSite(SiteName.Category, slug, id);
                    }
                    else if (link.type == "topic" && link.tableId == 3)
                    {
                        site = site_factory.CreateSite(SiteName.Topic, slug, id);
                    }
                    else if (link.type == "PostDetail" && link.tableId == 4)
                    {
                        site = site_factory.CreateSite(SiteName.PostDetail, slug, id);
                    }
                    return site.DirectTo();
                }
                else
                {
                    //slug k co tring ban link
                    site = site_factory.CreateSite(SiteName.Error, "", -1);
                    return site.DirectTo();
                }                
            }
        }
    }
}
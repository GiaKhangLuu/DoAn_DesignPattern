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
        private ISiteStrategy _site_strategy { get; set; }
        
        // GET: Site
        public ActionResult Index(int id, String slug = "")
        {
            int page = 1;
            //_site_strategy = new ErrorController();

            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = int.Parse(Request.QueryString["page"].ToString());
            }

            if (slug == "")
            {
                _site_strategy = new HomeController();
                return _site_strategy.Go_To_Site(slug, 0);
            }
            else
            {
                var rowlink = db.Link.Where(m => m.slug == slug);
                if (rowlink.Count() > 0)
                {
                    var link = rowlink.First();
                    if (link.type == "ProductDetail" && link.tableId == 1)
                    {
                        _site_strategy = new ProductDetailController();                       
                    }
                    else if (link.type == "category" && link.tableId == 2)
                    {
                        _site_strategy = new CategoryController();
                    }
                    else if (link.type == "topic" && link.tableId == 3)
                    {
                        _site_strategy = new PostCategoryController();
                    }
                    else if (link.type == "PostDetail" && link.tableId == 4)
                    {
                        _site_strategy = new PostDetailController();
                    }
                    return _site_strategy.Go_To_Site(slug, id);
                }
                else
                {
                    //slug k co tring ban link
                    return _site_strategy.Go_To_Site(slug, 0);
                }                
            }
        }
    }
}
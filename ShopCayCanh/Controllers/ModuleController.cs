using System.Linq;
using System.Web.Mvc;
using ShopCayCanh.Models;


namespace ShopCayCanh.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        //ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        public ActionResult Mainmenu()
        {

            var list = Singleton_Menu.GetInstance.list_menu;

            list = list.Where(m => m.status == 1).
                Where(m => m.parentid == 0 && m.position == "mainmenu")
                .OrderBy(m => m.orders).ToList();

            return View("_Mainmenu", list);
        }


        public ActionResult submainmenu(int parentid)
        {
            var list_menu = Singleton_Menu.GetInstance.list_menu;

            

            ViewBag.mainmenuitem = Singleton_Menu.GetInstance.Find(parentid);

            var list = list_menu.Where(m => m.status == 1).
                Where(m => m.parentid == parentid && m.position == "mainmenu")
                .OrderBy(m => m.orders);

            if (list.Count() != 0)
            {
                return View("~/Views/Module/Submenu/_submainmenu1.cshtml", list);
            }
            else
            {
                return View("~/Views/Module/Submenu/_submainmenu2.cshtml", list);
            }

        }


        public ActionResult Listcategory()
        {
            var list = Singleton_Category.GetInstance.list_cat;

            list = list.Where(m => m.status == 1).
               Where(m => m.parentid == 0)
               .OrderBy(m => m.orders).ToList();

            return View("_Listcategory", list);
        }


        public ActionResult ListcategoryAll()
        {
            var list = Singleton_Category.GetInstance.list_cat;

            list = list.Where(m => m.status == 1)
               .OrderBy(m => m.orders).ToList();

            return View("_LoaiSp", list);
        }
        public ActionResult SubListcategory(int parentid)
        {
            var list = Singleton_Category.GetInstance.list_cat;

            ViewBag.mainmenuitem = Singleton_Category.GetInstance.Find(parentid);

            list = list.Where(m => m.status == 1).
                Where(m => m.parentid == parentid)
                .OrderBy(m => m.orders).ToList();
            if (list.Count() != 0)
            {
                return View("~/Views/Module/SubCategory/_subcategory1.cshtml", list);
            }
            else
            {
                return View("~/Views/Module/SubCategory/_subcategory2.cshtml", list);
            }

        }
        public ActionResult slider()
        {
            var list = Single_Slider.GetInstance.list_slider;

            list = list
                .Where(m => m.status == 1 && m.position == "SliderShow").OrderBy(m => m.orders).ToList();

            return View("_Slider", list);
        }
        public ActionResult header()
        {
            if (Session["id"].Equals(""))
            {
                ViewBag.name = "";
            }
            else
            {
                ViewBag.name = Session["user"];
                ViewBag.id = int.Parse(Session["id"].ToString());
            }
            return View("_HeaderHome");
        }
    }
}
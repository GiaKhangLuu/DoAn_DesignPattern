using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Common;
using ShopCayCanh.Models;
using ShopCayCanh.Library;

namespace ShopCayCanh.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class MenuController : BaseController
    {
        private ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var list_menu = Singleton_Menu.GetInstance.list_menu;

            ViewBag.listCate = list_cat.Where(m => m.status == 1).ToList();
            ViewBag.listTopic = db.topics.Where(m => m.status == 1).ToList();
            ViewBag.listPage = db.posts.Where(m => m.status == 1 && m.type == "page").ToList();

            var list = list_menu.Where(m => m.status > 0).ToList();

            return View(list);
        }


        [HttpPost]
        public ActionResult Index(FormCollection data)
        {

            if (!string.IsNullOrEmpty(data["ADDPAGE"]))
            {
                
                var itemcatt = data["itempage"];

                if (itemcatt==null) { Message.set_flash("Bạn chưa chọn Mà", "danger");
                    return RedirectToAction("index");
                }

                var arrcat = itemcatt.Split(',');              
                IIterator<String> iterator = new StringIterator(arrcat.ToList());
                var item = iterator.First();
                while(!iterator.IsDone)
                {
                    int id = int.Parse(item);
                    Mpost post = db.posts.Find(id);
                    Mmenu menu = new Mmenu();

                    menu.name = post.title;
                    menu.link = post.slug;
                    menu.position = data["position"];
                    menu.type = "menu";
                    menu.tableid = 2;
                    menu.parentid = 0;
                    menu.orders = 1;
                    menu.created_at = DateTime.Now;
                    menu.updated_at = DateTime.Now;
                    menu.created_by = int.Parse(Session["Admin_id"].ToString());
                    menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                    menu.status = 1;

                    Singleton_Menu.GetInstance.Add(menu);

                    Message.set_flash("Thêm thành công", "success");

                    item = iterator.Next();
                }

            }

            if (!string.IsNullOrEmpty(data["THEMCATE"]))
            {
                var itemcatt = data["itemCat"];
                if (itemcatt == null)
                {
                    Message.set_flash("Bạn chưa chọn Mà", "danger");
                    return RedirectToAction("index");
                }

                var arrcat = itemcatt.Split(',');              
                IIterator<String> iterator = new StringIterator(arrcat.ToList());
                var item = iterator.First();
                while(!iterator.IsDone)
                {
                    int id = int.Parse(item);
                    Mcategory mcategory = Singleton_Category.GetInstance.Find(id);
                    Mmenu menu = new Mmenu();

                    menu.name = mcategory.name;
                    menu.link = "loaiSP/" + mcategory.slug;
                    menu.position = data["position"];
                    menu.type = "menu";
                    menu.tableid = 2;
                    menu.parentid = 0;
                    menu.orders = 1;
                    menu.created_at = DateTime.Now;
                    menu.updated_at = DateTime.Now;
                    menu.created_by = int.Parse(Session["Admin_id"].ToString());
                    menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                    menu.status = 1;

                    Singleton_Menu.GetInstance.Add(menu);

                    Message.set_flash("Thêm thành công", "success");

                    item = iterator.Next();
                }
               
            }

            if (!string.IsNullOrEmpty(data["THEMTOPIC"]))
            {
                var itemcatt = data["itemtopic"];
                if (itemcatt == null)
                {
                    Message.set_flash("Bạn chưa chọn", "danger");
                    return RedirectToAction("index");
                }
                var arrcat = itemcatt.Split(',');              
                IIterator<String> iterator = new StringIterator(arrcat.ToList());
                var item = iterator.First();
                while(!iterator.IsDone)
                {
                    int id = int.Parse(item);
                    Mtopic mtopic = db.topics.Find(id);
                    Mmenu menu = new Mmenu();

                    menu.name = mtopic.name;
                    menu.link = mtopic.slug;
                    menu.position = data["position"];
                    menu.type = "menu";
                    menu.tableid = 2;
                    menu.parentid = 0;
                    menu.orders = 1;
                    menu.created_at = DateTime.Now;
                    menu.updated_at = DateTime.Now;
                    menu.created_by = int.Parse(Session["Admin_id"].ToString());
                    menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                    menu.status = 1;

                    Singleton_Menu.GetInstance.Add(menu);

                    Message.set_flash("Thêm thành công", "success");

                    item = iterator.Next();
                }
              
            }

            if (!string.IsNullOrEmpty(data["THEMCUSS"]))
            {
                Mmenu menu = new Mmenu();

                menu.position = data["position"];
                menu.name = data["name"];
                menu.link = data["link"];
                menu.type = "menu";
                menu.tableid = 2;
                menu.parentid = 0;
                menu.orders = 1;
                menu.created_at = DateTime.Now;
                menu.updated_at = DateTime.Now;
                menu.created_by = int.Parse(Session["Admin_id"].ToString());
                menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                menu.status = 1;

                Singleton_Menu.GetInstance.Add(menu);

                Message.set_flash("Thêm thành công", "success");
            }

            var list_cat = Singleton_Category.GetInstance.list_cat;
            var list_menu = Singleton_Menu.GetInstance.list_menu;

            ViewBag.listCate = list_cat.Where(m => m.status == 1).ToList();
            ViewBag.listTopic = db.topics.Where(m => m.status == 1).ToList();
            ViewBag.listPage = db.posts.Where(m => m.status == 1 && m.type == "post").ToList();

            var list = list_menu.Where(m => m.status > 0).ToList();

            return View(list);
        }


        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mmenu mmenu = Singleton_Menu.GetInstance.Find(id);
            if (mmenu == null)
            {
                return HttpNotFound();
            }
            return View(mmenu);
        }


        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mmenu mmenu = Singleton_Menu.GetInstance.Find(id);

            if (mmenu == null)
            {
                return HttpNotFound();
            }

            var list_menu = Singleton_Menu.GetInstance.list_menu;
            ViewBag.listMenu = list_menu.Where(m => m.status != 0).ToList();

            return View(mmenu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,type,link,tableid,parentid,orders,position,created_at,created_by,updated_at,updated_by,status")] Mmenu mmenu)
        {
            if (ModelState.IsValid)
            {
                mmenu.updated_at = DateTime.Now;
                mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());

                db.Entry(mmenu).State = EntityState.Modified;
                db.SaveChanges();
                Singleton_Menu.GetInstance.Refresh();

                Message.set_flash("Chỉnh sửa thành công", "success");

                return RedirectToAction("Index");
            }

            return View(mmenu);
        }

        public ActionResult Status(int id)
        {
            Mmenu mmenu = Singleton_Menu.GetInstance.Find(id);

            mmenu.status = (mmenu.status == 1) ? 2 : 1;
            mmenu.updated_at = DateTime.Now;
            mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mmenu).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Menu.GetInstance.Refresh();

            Message.set_flash("Thay đổi trang thái thành công", "success");

            return RedirectToAction("Index");
        }

        //trash
        public ActionResult trash()
        {
            var list_menu = Singleton_Menu.GetInstance.list_menu;
            var list = list_menu.Where(m => m.status == 0).ToList();

            return View("Trash", list);
        }

        public ActionResult Deltrash(int id)
        {
            Mmenu mmenu = Singleton_Menu.GetInstance.Find(id);

            mmenu.status = 0;
            mmenu.updated_at = DateTime.Now;
            mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mmenu).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Menu.GetInstance.Refresh();

            Message.set_flash("Xóa thành công", "success");

            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Mmenu mmenu = Singleton_Menu.GetInstance.Find(id);

            mmenu.status = 2;
            mmenu.updated_at = DateTime.Now;
            mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mmenu).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Menu.GetInstance.Refresh();

            Message.set_flash("khôi phục thành công", "success");

            return RedirectToAction("trash");
        }

        public ActionResult deleteTrash(int id)
        {
            Mmenu mmenu = Singleton_Menu.GetInstance.Find(id);

            Singleton_Menu.GetInstance.Remove(mmenu);

            Message.set_flash("Đã xóa vĩnh viễn 1 Menu", "success");

            return RedirectToAction("trash");
        }
    }
}

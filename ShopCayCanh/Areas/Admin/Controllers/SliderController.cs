using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Mvc;
using ShopCayCanh.Models;
using ShopCayCanh.Common;

namespace ShopCayCanh.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class SliderController : BaseController
    {
        private ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            var list_slider = Singleton_Slider.GetInstance.list_slider;
            var list = list_slider.Where(m => m.status != 0).OrderByDescending(m => m.ID).ToList();

            return View(list);
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mslider mslider = Singleton_Slider.GetInstance.Find(id);
            if (mslider == null)
            {
                return HttpNotFound();
            }

            return View(mslider);
        }

        // GET: Admin/Slider/Create
        public ActionResult Create()
        {
            var list_slider = Singleton_Slider.GetInstance.list_slider;
            ViewBag.listCate = list_slider.Where(m => m.status != 0 ).ToList();

            return View();
        }

        // POST: Admin/Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mslider mslider, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string slug = Mystring.ToSlug(mslider.name.ToString());
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images"), namefilenew);

                file.SaveAs(path);
                mslider.url = slug;
                mslider.img = namefilenew;
                mslider.created_at = DateTime.Now;
                mslider.updated_at = DateTime.Now;
                mslider.created_by = int.Parse(Session["Admin_id"].ToString());
                mslider.updated_by = int.Parse(Session["Admin_id"].ToString());

                Singleton_Slider.GetInstance.Add(mslider);

                Message.set_flash("Thêm thành công", "success");

                return RedirectToAction("Index");
            }

            Message.set_flash("Thêm thất bại", "danger");

            return View(mslider);
        }

        // GET: Admin/Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            var list_slider = Singleton_Slider.GetInstance.list_slider;
            ViewBag.listCate = list_slider.Where(m => m.status != 0).ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mslider mslider = Singleton_Slider.GetInstance.Find(id);
            if (mslider == null)
            {
                return HttpNotFound();
            }
           
            return View(mslider);
        }

        // POST: Admin/Slider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mslider mslider)
        {
           
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(mslider.name.ToString());
                HttpPostedFileBase file = Request.Files["img"];
                string filename = file.FileName.ToString();

                if (filename.Equals("") == false)
                {
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images"), namefilenew);
                    file.SaveAs(path);
                    mslider.img = namefilenew;
                }

                mslider.url = slug;
                mslider.updated_at = DateTime.Now;
                mslider.updated_by = int.Parse(Session["Admin_id"].ToString());

                db.Entry(mslider).State = EntityState.Modified;
                db.SaveChanges();
                Singleton_Slider.GetInstance.Refresh();

                return RedirectToAction("Index");
            }

            var list_slider = Singleton_Slider.GetInstance.list_slider;
            ViewBag.listCate = list_slider.Where(m => m.status != 0).ToList();

            return View(mslider);
        }

        // status
        public ActionResult Status(int id)
        {
            Mslider mslider = Singleton_Slider.GetInstance.Find(id);

            mslider.status = (mslider.status == 1) ? 2 : 1;
            mslider.updated_at = DateTime.Now;
            mslider.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mslider).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Slider.GetInstance.Refresh();

            Message.set_flash("Thay đổi trang thái thành công", "success");

            return RedirectToAction("Index");
        }

        // trash
        public ActionResult trash()
        {
            var list_slider = Singleton_Slider.GetInstance.list_slider;
            var list = list_slider.Where(m => m.status == 0).ToList();

            return View("Trash", list);
        }

        // del trash
        public ActionResult Deltrash(int id)
        {
            Mslider mslider = Singleton_Slider.GetInstance.Find(id);

            mslider.status = 0;
            mslider.updated_at = DateTime.Now;
            mslider.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mslider).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Slider.GetInstance.Refresh();

            Message.set_flash("Xóa thành côngss", "success");

            return RedirectToAction("Index");
        }

        // retrash
        public ActionResult Retrash(int id)
        {
            Mslider mslider = Singleton_Slider.GetInstance.Find(id);

            mslider.status = 2;
            mslider.updated_at = DateTime.Now;
            mslider.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mslider).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Slider.GetInstance.Refresh();

            Message.set_flash("khôi phục thành công", "success");

            return RedirectToAction("trash");
        }

        // delete trash
        public ActionResult deleteTrash(int id)
        {
            Mslider mslider = Singleton_Slider.GetInstance.Find(id);

            Singleton_Slider.GetInstance.Remove(mslider);

            Message.set_flash("Đã xóa vĩnh viễn 1 Ảnh bìa", "success");

            return RedirectToAction("trash");
        }

        // GET: Admin/Slider/Duplicate/5
        public ActionResult Duplicate(int? id)
        {
            var list_slider = Singleton_Slider.GetInstance.list_slider;
            ViewBag.listCate = list_slider.Where(m => m.status != 0).ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mslider mslider = Singleton_Slider.GetInstance.Find(id);
            if (mslider == null)
            {
                return HttpNotFound();
            }

            return View(mslider);
        }

        // POST: Admin/Slider/Duplicate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duplicate(Mslider mslider)
        {

            if (ModelState.IsValid)
            {
                Mslider slider = Singleton_Slider.GetInstance.Find(mslider.ID);
                var clone_slider = (Mslider)slider.Clone();
                Singleton_Slider.GetInstance.Add(clone_slider);

                Message.set_flash("Nhân bản thành công", "success");
                return RedirectToAction("Index");
            }

            var list_slider = Singleton_Slider.GetInstance.list_slider;
            ViewBag.listCate = list_slider.Where(m => m.status != 0).ToList();

            return View(mslider);
        }

    }
}

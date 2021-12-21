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

namespace ShopCayCanh.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class TopicController : BaseController
    {
        private ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        // GET: Admin/Topic
        public ActionResult Index()
        {
            var list_topic = Singleton_Topic.GetInstance.list_topic;
            var list = list_topic.Where(m => m.status !=0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        // GET: Admin/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mtopic mtopic = Singleton_Topic.GetInstance.Find(id);
            if (mtopic == null)
            {
                return HttpNotFound();
            }
            return View(mtopic);
        }

        // GET: Admin/Topic/Create
        public ActionResult Create()
        {
            var list_topic = Singleton_Topic.GetInstance.list_topic;
            ViewBag.listtopic = list_topic.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mtopic mtopic)
        {
            var list_cat = Singleton_Category.GetInstance.list_cat;
            var list_topic = Singleton_Topic.GetInstance.list_topic;
            if (ModelState.IsValid)
            {
                //category
                string slug = Mystring.ToSlug(mtopic.name.ToString());
                if (list_cat.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Chủ đề đã tồn tại trong bảng Category", "danger");
                    return View(mtopic);
                }

                //topic

                if (list_topic.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Chủ đề đã tồn tại trong bảng Topic", "danger");
                    return View(mtopic);
                }

                if (db.Products.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Chủ đề đã tồn tại trong bảng Product", "danger");
                    return View(mtopic);
                }

                mtopic.slug = slug;
                mtopic.created_at = DateTime.Now;
                mtopic.updated_at = DateTime.Now;
                mtopic.created_by = int.Parse(Session["Admin_id"].ToString());
                mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());

                Singleton_Topic.GetInstance.Add(mtopic);

                Message.set_flash("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Thêm thất bại", "danger");

            ViewBag.listtopic = list_topic.Where(m => m.status != 0).ToList();
            return View(mtopic);
        }

        // GET: Admin/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mtopic mtopic = Singleton_Topic.GetInstance.Find(id);
            if (mtopic == null)
            {
                return HttpNotFound();
            }

            ViewBag.listtopic = Singleton_Topic.GetInstance.list_topic.
                Where(m => m.status != 0).ToList();
            return View(mtopic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Mtopic mtopic)
        {
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(mtopic.name.ToString());
                mtopic.slug = slug;
                mtopic.updated_at = DateTime.Now;
                mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());

                db.Entry(mtopic).State = EntityState.Modified;
                db.SaveChanges();
                Singleton_Topic.GetInstance.Refresh();

                return RedirectToAction("Index");
            }
            ViewBag.listtopic = Singleton_Topic.GetInstance.list_topic.
                Where(m => m.status != 0).ToList();
            return View(mtopic);
        }

        public ActionResult Status(int id)
        {
            Mtopic mtopic = Singleton_Topic.GetInstance.Find(id);
            mtopic.status = (mtopic.status == 1) ? 2 : 1;
            mtopic.updated_at = DateTime.Now;
            mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mtopic).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Topic.GetInstance.Refresh();

            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }

        //trash
        public ActionResult trash()
        {
            var list = Singleton_Topic.GetInstance.list_topic.
                Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }

        public ActionResult Deltrash(int id)
        {
            Mtopic mtopic = Singleton_Topic.GetInstance.Find(id);
            mtopic.status = 0;
            mtopic.updated_at = DateTime.Now;
            mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mtopic).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Topic.GetInstance.Refresh();

            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Mtopic mtopic = Singleton_Topic.GetInstance.Find(id);
            mtopic.status = 2;
            mtopic.updated_at = DateTime.Now;
            mtopic.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mtopic).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Topic.GetInstance.Refresh();

            Message.set_flash("Khôi phục thành Công", "success");
            return RedirectToAction("trash");
        }

        public ActionResult deleteTrash(int id)
        {
            Mtopic mtopic = Singleton_Topic.GetInstance.Find(id);

            Singleton_Topic.GetInstance.Remove(mtopic);

            Message.set_flash("Đã xóa vĩnh viễn 1 Chủ đề", "success");
            return RedirectToAction("trash");
        }
    }
}

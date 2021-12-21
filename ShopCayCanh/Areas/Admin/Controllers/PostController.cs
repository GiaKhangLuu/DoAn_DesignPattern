using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Common;
using ShopCayCanh.Models;

namespace ShopCayCanh.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class PostController : BaseController
    {
        private ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        // GET: Admin/Post
        public ActionResult Index()
        {
            var list = db.posts.Where(m => m.status > 0).OrderByDescending(m=>m.ID).ToList();
            return View(list);
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mpost mpost = db.posts.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            return View(mpost);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.listTopic = Singleton_Topic.GetInstance.list_topic.Where(m => m.status != 0 ).ToList();
            return View();
        }

        // POST: Admin/Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Mpost mpost)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                var namecateDb = Singleton_Topic.GetInstance.list_topic.Where(m => m.ID == mpost.topid).First();
                string slug = Mystring.ToSlug(mpost.title.ToString());
                string namecate = Mystring.ToStringNospace(namecateDb.name);
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/post/"), namefilenew);
                var folder = Server.MapPath("~/public/images/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                mpost.img = namefilenew;
                mpost.slug = slug;
                mpost.type = "Post";
                mpost.created_at = DateTime.Now;
                mpost.updated_at = DateTime.Now;
                mpost.created_by = int.Parse(Session["Admin_id"].ToString());
                mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.posts.Add(mpost);
                db.SaveChanges();

                // Add link to db
                link link = new link();
                link.slug = mpost.slug;
                link.tableId = 4;
                link.type = "PostDetail";
                link.parentId = mpost.ID;
                db.Link.Add(link);
                db.SaveChanges();

                Message.set_flash("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = Singleton_Topic.GetInstance.list_topic.Where(m => m.status != 0).ToList();
            Message.set_flash("Thêm Thất Bại", "danger");
            return View(mpost);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mpost mpost = db.posts.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            ViewBag.listTopic = Singleton_Topic.GetInstance.list_topic.Where(m => m.status != 0).ToList();
            return View(mpost);
        }

        // POST: Admin/Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit( Mpost mpost)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                string slug = Mystring.ToSlug(mpost.title.ToString());
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    var namecateDb = db.topics.Where(m => m.ID == mpost.topid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images/post"), namefilenew);
                    var folder = Server.MapPath("~/public/images/post/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    mpost.img = namefilenew;
                }
                mpost.slug = slug;
                mpost.updated_at = DateTime.Now;
                mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(mpost).State = EntityState.Modified;
                db.SaveChanges();

                // Edit link in db
                link modified_link = db.Link.FirstOrDefault(l => l.parentId == mpost.ID &&
                    l.tableId == 4);
                if (modified_link != null)
                {
                    modified_link.slug = mpost.slug;
                    db.Entry(modified_link).State = EntityState.Modified;
                    db.SaveChanges();
                }

                Message.set_flash("Sửa thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.listTopic = Singleton_Topic.GetInstance.list_topic.Where(m => m.status != 0).ToList();
            Message.set_flash("Sửa Thất Bại", "danger");
            return View(mpost);
        }

        // delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mpost mpost = db.posts.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            return View(mpost);
        }

        // status
        public ActionResult Status(int id)
        {
            Mpost mpost = db.posts.Find(id);
            mpost.status = (mpost.status == 1) ? 2 : 1;
            mpost.updated_at = DateTime.Now;
            mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }

        // trash
        public ActionResult trash()
        {
            var list = db.posts.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }

        // del trash
        public ActionResult Deltrash(int id)
        {
            Mpost mpost = db.posts.Find(id);
            mpost.status = 0;
            mpost.updated_at = DateTime.Now;
            mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        // retrash
        public ActionResult Retrash(int id)
        {
            Mpost mpost = db.posts.Find(id);
            mpost.status = 2;
            mpost.updated_at = DateTime.Now;
            mpost.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }

        // delete trash
        public ActionResult deleteTrash(int id)
        {
            Mpost mpost = db.posts.Find(id);
            db.posts.Remove(mpost);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 sản phẩm", "success");
            return RedirectToAction("trash");
        }

        // GET: Admin/Post/Duplicate/5
        public ActionResult Duplicate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mpost mpost = db.posts.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            ViewBag.listTopic = Singleton_Topic.GetInstance.list_topic.Where(m => m.status != 0).ToList();
            return View(mpost);
        }

        // POST: Admin/Post/Duplicate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Duplicate(Mpost mpost)
        {
            if (ModelState.IsValid)
            {
                var post = db.posts.Find(mpost.ID);
                var clone_post = (Mpost)post.Clone();

                db.posts.Add(clone_post);
                db.SaveChanges();

                // Add link to db
                link link = new link();
                link.slug = clone_post.slug;
                link.tableId = 4;
                link.type = "PostDetail";
                link.parentId = clone_post.ID;
                db.Link.Add(link);
                db.SaveChanges();

                Message.set_flash("Nhân bản thành công", "success");
                return RedirectToAction("Index");
            }
            ViewBag.listtopic = Singleton_Topic.GetInstance.list_topic.
                Where(m => m.status != 0).ToList();
            return View(mpost);
        }
    }
}

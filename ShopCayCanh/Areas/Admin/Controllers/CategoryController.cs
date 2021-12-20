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

    public class CategoryController : BaseController
    {
        private ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        // GET: Admin/Category
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
        public ActionResult Index()
        {
            var list = Singleton_Category.GetInstance.list_cat;
            
            ViewBag.listCate = list.Where(m => m.status != 0).ToList();
            
            list = Singleton_Category.GetInstance.list_cat;

            list = list.Where(m => m.status > 0).ToList();

            return View(list);
        }


        // GET: Admin/Category/Create
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]     
        public ActionResult Create()
        {
            
            ViewBag.listCate = Singleton_Category.GetInstance.list_cat
                .Where(m => m.status !=0 ).ToList();

            return View();
        }


        // POST: Admin/Category/Create
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Mcategory mcategory)
        {
          
            if (ModelState.IsValid)
            {
                //category
                var list = Singleton_Category.GetInstance.list_cat;
                string slug = Mystring.ToSlug(mcategory.name.ToString());

                if (list.Where(m=>m.slug == slug).Count()>0) {
                    Message.set_flash("Loại sản phẩm đã tồn tại trong bảng Category", "danger");
                    return View(mcategory);
                }


                //topic
                if (db.topics.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Loại sản phẩm đã tồn tại trong bảng Topic", "danger");
                    return View(mcategory);
                }

                if (db.Products.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Loại sản phẩm đã tồn tại trong bảng Product", "danger");
                    return View(mcategory);
                }

                mcategory.slug = slug;
                mcategory.created_at = DateTime.Now;
                mcategory.updated_at = DateTime.Now;
                mcategory.created_by = int.Parse(Session["Admin_id"].ToString());
                mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());

                Singleton_Category.GetInstance.Add(mcategory);

                Message.set_flash("Thêm  thành công", "success");

                // Add link to db
                link link = new link();
                link.slug = mcategory.slug;
                link.tableId = 2;
                link.type = "category";
                link.parentId = mcategory.ID;
                db.Link.Add(link);
                db.SaveChanges();

                return RedirectToAction("index");
            }

            Message.set_flash("Thêm  Thất Bại", "danger");

            ViewBag.listCate = Singleton_Category.GetInstance.list_cat.Where(m => m.status != 0).ToList();

            return View(mcategory);
        }


        // GET: Admin/Category/Edit/5
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]      
        public ActionResult Edit(int? id)
        {
            ViewBag.listCate = Singleton_Category.GetInstance.list_cat.Where(m => m.status != 0).ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            if (mcategory == null)
            {
                return HttpNotFound();
            }

            return View(mcategory);
        }


        // POST: Admin/Category/Edit/5
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Mcategory mcategory)
        {
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(mcategory.name.ToString());

                mcategory.slug = slug;
                mcategory.updated_at = DateTime.Now;
                mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());

                db.Entry(mcategory).State = EntityState.Modified;
                db.SaveChanges();
                Singleton_Category.GetInstance.Refresh();

                // Edit link in db
                link modified_link = db.Link.FirstOrDefault(l => l.parentId == mcategory.ID &&
                    l.tableId == 2);
                if (modified_link != null)
                {
                    modified_link.slug = mcategory.slug;
                    db.Entry(modified_link).State = EntityState.Modified;
                    db.SaveChanges();
                }

                Message.set_flash("Sửa thành công", "success");
                return RedirectToAction("Index");
            }

            Message.set_flash("Sửa thất bại", "danger");

            return View(mcategory);
        }


        //status
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]       
        public ActionResult Status(int id)
        {
            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            mcategory.status = (mcategory.status == 1) ? 2 : 1;
            mcategory.updated_at = DateTime.Now;
            mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mcategory).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Category.GetInstance.Refresh();

            Message.set_flash("Thay đổi trang thái thành công", "success");

            return RedirectToAction("Index");
        }


        //trash
        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]     
        public ActionResult trash()
        {
            var list = Singleton_Category.GetInstance.list_cat.Where(m => m.status == 0).ToList();

            return View("Trash", list);
        }


        public ActionResult Deltrash(int id)
        {
            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            mcategory.status =  0;
            mcategory.updated_at = DateTime.Now;
            mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mcategory).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Category.GetInstance.Refresh();

            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }
       

        public ActionResult Retrash(int id)
        {
            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            mcategory.status = 2;
            mcategory.updated_at = DateTime.Now;
            mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mcategory).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Category.GetInstance.Refresh();

            Message.set_flash("khôi phục thành công", "success");

            return RedirectToAction("trash");
        }


        public ActionResult deleteTrash(int id)
        {
            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            Singleton_Category.GetInstance.Remove(mcategory);

            Message.set_flash("Đã xóa vĩnh viễn 1 sản phẩm", "success");

            return RedirectToAction("trash");
        }


        // GET: Admin/Category/Duplicate/5
        public ActionResult Duplicate(int? id)
        {
            ViewBag.listCate = Singleton_Category.GetInstance.list_cat.Where(m => m.status != 0).ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            if (mcategory == null)
            {
                return HttpNotFound();
            }

            return View(mcategory);
        }


        // POST: Admin/Category/Duplicate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Duplicate(Mcategory mcategory)
        {          
            if (ModelState.IsValid)
            {
                var category = Singleton_Category.GetInstance.Find(mcategory.ID);
                var clone_category = (Mcategory)category.Clone();

                Singleton_Category.GetInstance.Add(clone_category);

                // Add link to db
                link link = new link();
                link.slug = clone_category.slug;
                link.tableId = 2;
                link.type = "category";
                link.parentId = clone_category.ID;
                db.Link.Add(link);
                db.SaveChanges();

                Message.set_flash("Nhân bản thành công", "success");
                return RedirectToAction("index");
            }

            Message.set_flash("Nhân bản thất bại", "danger");
            return View(mcategory);
        }

    }
}

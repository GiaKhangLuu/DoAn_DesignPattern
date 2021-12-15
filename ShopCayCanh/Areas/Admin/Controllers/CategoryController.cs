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


        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            
            ViewBag.listCate = Singleton_Category.GetInstance.list_cat
                .Where(m => m.status !=0 ).ToList();

            return View();
        }


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

                //db.Categorys.Add(mcategory);
                //db.SaveChanges();

                Singleton_Category.GetInstance.Add(mcategory);

                Message.set_flash("Thêm  thành công", "success");

                return RedirectToAction("index");
            }

            Message.set_flash("Thêm  Thất Bại", "danger");

            ViewBag.listCate = Singleton_Category.GetInstance.list_cat.Where(m => m.status != 0).ToList();

            return View(mcategory);
        }


        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
        // GET: Admin/Category/Edit/5
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

                return RedirectToAction("Index");
            }

            Message.set_flash("Sửa thất bại", "success");

            return View(mcategory);
        }


        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
        //status
        public ActionResult Status(int id)
        {
            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            mcategory.status = (mcategory.status == 1) ? 2 : 1;
            mcategory.updated_at = DateTime.Now;
            mcategory.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mcategory).State = EntityState.Modified;
            db.SaveChanges();

            Message.set_flash("Thay đổi trang thái thành công", "success");

            return RedirectToAction("Index");
        }


        [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
        //trash
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

            Message.set_flash("khôi phục thành công", "success");

            return RedirectToAction("trash");
        }


        public ActionResult deleteTrash(int id)
        {
            Mcategory mcategory = Singleton_Category.GetInstance.Find(id);

            //db.Categorys.Remove(mcategory);
            //db.SaveChanges();

            Singleton_Category.GetInstance.Remove(mcategory);

            Message.set_flash("Đã xóa vĩnh viễn 1 sản phẩm", "success");

            return RedirectToAction("trash");
        }

    }
}

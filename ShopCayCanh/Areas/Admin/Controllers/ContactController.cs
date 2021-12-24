﻿using System;
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
    public class ContactController : BaseController
    {
        private ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        // GET: Admin/Contact
   
        public ActionResult Index()
        {
            var list_contact = Singleton_Contact.GetInstance.list_contact;
            var list = list_contact.Where(m => m.status > 0).ToList();

            return View(list);
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mcontact mcontact = Singleton_Contact.GetInstance.Find(id);
            if (mcontact == null)
            {
                return HttpNotFound();
            }

            return View(mcontact);
        }
  
        public ActionResult Status(int id)
        {
            Mcontact mcontact = Singleton_Contact.GetInstance.Find(id);

            mcontact.status = (mcontact.status == 1) ? 2 : 1;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Contact.GetInstance.Refresh();

            Message.set_flash("Thay đổi trang thái thành công", "success");

            return RedirectToAction("Index");
        }

        //trash
        public ActionResult trash()
        {
            var list_contact = Singleton_Contact.GetInstance.list_contact;
            var list = list_contact.Where(m => m.status == 0).ToList();

            return View("Trash", list);
        }

        public ActionResult Deltrash(int id)
        {
            Mcontact mcontact = Singleton_Contact.GetInstance.Find(id);

            mcontact.status = 0;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Contact.GetInstance.Refresh();

            Message.set_flash("Xóa thành công", "success");

            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Mcontact mcontact = Singleton_Contact.GetInstance.Find(id);

            mcontact.status = 2;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());

            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Singleton_Contact.GetInstance.Refresh();

            Message.set_flash("khôi phục thành công", "success");

            return RedirectToAction("trash");
        }

        public ActionResult deleteTrash(int id)
        {
            Mcontact mcontact = Singleton_Contact.GetInstance.Find(id);
            Singleton_Contact.GetInstance.Remove(mcontact);

            Message.set_flash("Đã xóa vĩnh viễn 1 Liên Hệ", "success");

            return RedirectToAction("trash");
        }

        // GET: Admin/Contact/Duplicate/5
        public ActionResult Duplicate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mcontact mcontact = Singleton_Contact.GetInstance.Find(id);
            if (mcontact == null)
            {
                return HttpNotFound();
            }

            return View(mcontact);
        }


        // POST: Admin/Products/Duplicate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Duplicate(int ID, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var contact = Singleton_Contact.GetInstance.Find(ID);
                var clone_contact = (Mcontact)contact.Clone();

                Singleton_Contact.GetInstance.Add(clone_contact);
             
                Message.set_flash("Nhân bản thành công", "success");
                return RedirectToAction("index");
            }

            Message.set_flash("Nhân bản thất bại", "danger");
            return RedirectToAction("index");
        }
    }
}

using ShopCayCanh.Common;
using ShopCayCanh.Controllers;
using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopCayCanh.Areas.Admin.Controllers
{
    public class AuthController : AuthTemplateMethodController
    {
        // GET: Admin/Auth
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        public ActionResult login()
        {
            return View("_login");
        }

        [HttpPost]
        public ActionResult login(FormCollection fc)
        {
            return Login(fc);        
        }

        public ActionResult logout()
        {
            Session["Admin_id"] = "";
            Session["Admin_user"] = "";
            Response.Redirect("~/Admin");
            return View();
        }
 
        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.users.Find(id);
            ViewBag.role = db.roles.Where(m => m.parentId == muser.access).First();
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View("_information", muser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Muser muser)
        {
            if (ModelState.IsValid)
            {
                muser.img = "ádasd";
                muser.access = 0;
                muser.created_at = DateTime.Now;
                muser.updated_at = DateTime.Now;
                muser.created_by = int.Parse(Session["Admin_id"].ToString());
                muser.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(muser).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Cập nhật thành công", "success");
                ViewBag.role = db.roles.Where(m => m.parentId == muser.access).First();
                return View("_information", muser);
            }
            Message.set_flash("Cập nhật Thất Bại", "danger");
            ViewBag.role = db.roles.Where(m => m.parentId == muser.access).First();
            return View("_information", muser);
        }

        protected override Muser CheckLogin(FormCollection fc)
        {
            String Username = fc["username"];
            string Pass = Mystring.ToMD5(fc["password"]);
            var user_account = db.users.FirstOrDefault(m => m.access != 1 && 
                                                       m.status == 1 && 
                                                       m.username == Username);
            var userC = db.users.FirstOrDefault(m => m.username == Username && m.access == 1);

            if (userC != null)
            {
                ViewBag.error = "Bạn không có quyền đăng nhập";
                return null;
            }
            if (user_account == null)
            {
                ViewBag.error = "Tên Đăng Nhập Không Đúng";
            }
            else
            {
                if (user_account.password.Equals(Pass) &&
                    user_account.status == 1 &&
                    user_account.access != 1)
                {
                    return user_account;
                }
                ViewBag.error = "Mật Khẩu Không Đúng";
            }
            return null;
        }

        protected override void SaveSession(Muser muser)
        {
            if(muser != null)
            {
                role role = db.roles.Where(m => m.parentId == muser.access).First();
                var userSession = new Userlogin();
                userSession.UserName = muser.username;
                userSession.UserID = muser.ID;
                userSession.GroupID = role.GropID;
                userSession.AccessName = role.accessName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                var i = Session["SESSION_CREDENTIALS"];
                Session["Admin_id"] = muser.ID;
                Session["Admin_user"] = muser.username;
                Session["Admin_fullname"] = muser.fullname;
                Response.Redirect("~/Admin");
            }
        }

        protected override ActionResult GoToSite()
        {
            ViewBag.sess = Session["Admin_id"];
            return View("_login");
        }
    }
}
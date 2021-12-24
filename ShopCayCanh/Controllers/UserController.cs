using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShopCayCanh.Library;
using System.Threading.Tasks;
using System.Net;

namespace ShopCayCanh.Controllers
{
    public class UserController : BaseController
    {

        ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View("index", muser);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Muser muser)
        {
            if (ModelState.IsValid)
            {
                muser.img = "ádasd";
                muser.access = 1;
                muser.created_at = DateTime.Now;
                muser.updated_at = DateTime.Now;
                muser.created_by = int.Parse(Session["id"].ToString());
                muser.updated_by = int.Parse(Session["id"].ToString());

                // Initialize PROXY object
                var proxy_user = new ProxyUser(muser);
                var status_code = proxy_user.Edit(db);
                if (status_code == UserStatusCode.EDIT_SUCCESSFULLY)
                {
                    Message.set_flash(status_code, "success");
                }
                else
                {
                    Message.set_flash(status_code, "error");
                }

                return View("index", muser);
            }
            return View("index", muser);
        }

        // change password
        public ActionResult ChangePassWord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View("_changePassword", muser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassWord(Muser muser, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                string oldPass = Mystring.ToMD5(fc["passOld"]);
                string rePass = Mystring.ToMD5(fc["rePass"]);
                string newPass = Mystring.ToMD5(fc["password1"]);

                var user = db.users.Find(muser.ID);

                // Initialize Proxy object
                var proxy_user = new ProxyUser(user);
                string status_code = await proxy_user.ChangePassword(db, oldPass, rePass, newPass);

                if (status_code == UserStatusCode.CHANGE_PASSWORD_SUCCESSFULLY)
                {
                    Message.set_flash(status_code, "success");
                    return Redirect("~/tai-khoan/" + muser.ID + "");
                }
                else
                {
                    ViewBag.status = status_code;
                    return View("_changePassword", muser);
                }
            }
            return View("_changePassword", muser);
        }
    }
}
using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using ShopCayCanh.Library;
using ShopCayCanh.Controllers.AuthControllerFacade;

namespace ShopCayCanh.Controllers
{
    public class AuthController : AuthTemplateMethodController
    {
        AuthFacade auth_facade = new AuthFacade();

        public void login(FormCollection fc)
        {
            Login(fc);
        }

        public void logout()
        {
            Session["id"] = "";
            Session["user"] = "";
            Response.Redirect("~/");
            Message.set_flash("Đăng xuất thành công", "success");
        }

        public void register(Muser muser, FormCollection fc)
        {         
            if (ModelState.IsValid)
            {               
                var status_code = auth_facade.Register(muser, fc);
                if(status_code == UserStatusCode.REGISTER_SUCCESSFULLY)
                {
                    Message.set_flash(status_code, "success");
                    Response.Redirect("~/");
                }              
                else
                {
                    Message.set_flash(status_code, "error");
                    Response.Redirect("~/");
                }
            }
        }

        public ActionResult forgetpass()
        {
            return View();
        }

        public ActionResult newPasswordFG(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = auth_facade.FindById(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View("_newPasswordFG", muser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> newPasswordFG(Muser muser, FormCollection fc)
        {
            if(ModelState.IsValid)
            {
                var user = auth_facade.FindById(muser.ID);
                var status_code = await auth_facade.ResetPassword(user, fc);
                if (status_code == UserStatusCode.RESET_SUCCESSFULLY)
                {
                    Message.set_flash(status_code, "success");
                    return Redirect("~/");
                }
                else
                {
                    ViewBag.status = status_code;
                    return View("_newPasswordFG", muser);
                }
            }
            ViewBag.status = UserStatusCode.TRY_AGAIN;
            return View("_newPasswordFG", muser);
        }

        public ActionResult sendMail()
        {
            ViewBag.mess = "";
            var username = Request.Form["username"];
            var item = auth_facade.Find_Client_By_Username(username);
            if (item == null)
            {
                ViewBag.mess = "Tên đăng nhập không tồn tại";
                return View("forgetPass");
            }
            else
            {
                auth_facade.SendMail(item);
                ViewBag.mess = item.email;
                return View("sendMailFinish");
            }
        }

        protected override Muser CheckLogin(FormCollection fc)
        {
            string Username = fc["uname"];
            string Pass = Mystring.ToMD5(fc["psw"]);
            string PassNoMD5 = fc["psw"];

            var user_account = auth_facade.Find_Client_By_Username(Username);

            if (user_account == null)
            {
                Message.set_flash("Tên đăng nhập không tồn tại", "error");
            } 
            else
            {
                if (user_account.password.Equals(Pass) && 
                    user_account.status == 1 &&
                    user_account.access == 1)
                {
                    return user_account;
                }
                Message.set_flash("Mật khẩu không đúng", "error");
            }
            return null;
        }

        protected override void SaveSession(Muser muser)
        {
            if(muser != null)
            {
                Session["id"] = muser.ID;
                Session["user"] = muser.username;
                ViewBag.name = Session["user"];
                if (!Response.IsRequestBeingRedirected)
                {
                    Message.set_flash("Đăng nhập thành công", "success");
                }                 
                Response.Redirect("~/");
                return;
            }
            if (!Response.IsRequestBeingRedirected)
                Response.Redirect("~/");
        }

        protected override ActionResult GoToSite()
        {
            return null;
        }
    }
}
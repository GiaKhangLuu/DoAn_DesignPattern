using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShopCayCanh.Controllers.AuthControllerFacade
{
    public class AuthFacade
    {
        SubContext sub_context;
        SubUser sub_user;
        SubMail sub_mail;
        SubSmtpClient sub_smtp_client;

        public AuthFacade()
        {
            sub_context = new SubContext();
            sub_user = new SubUser();
            sub_mail = new SubMail();
            sub_smtp_client = new SubSmtpClient();
        }

        // ======================= Sub-User ==============================
        public string Register(Muser muser, FormCollection fc)
        {
            var context = sub_context.Context;
            return sub_user.Register(muser, fc, context);
        }

        public async Task<string> ResetPassword(Muser muser, FormCollection fc)
        {
            var context = sub_context.Context;
            return (await sub_user.ResetPassword(muser, fc, context));
        }

        // ======================= Sub-Context ==============================
        public Muser FindById(int? id)
        {
            return sub_context.FindById(id);
        }

        public Muser Find_Client_By_Username(string username)
        {
            return sub_context.Find_Client_By_Username(username);
        }

        // ======================= Mail ==============================
        public void SendMail(Muser item)
        {
            var mail_msg = sub_mail.CreateMailMessage(item);
            sub_smtp_client.SendMail(mail_msg);
        }
    }
}
using ShopCayCanh.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ShopCayCanh.Controllers.AuthControllerFacade
{
    public class SubSmtpClient
    {
        SmtpClient smtp;

        public void SendMail(MailMessage mail_msg)
        {
            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;

            // Email dùng để gửi đi
            NetworkCredential nc = new NetworkCredential(Util.email, Util.password);
            smtp.Credentials = nc;
            smtp.Send(mail_msg);
        }
    }
}
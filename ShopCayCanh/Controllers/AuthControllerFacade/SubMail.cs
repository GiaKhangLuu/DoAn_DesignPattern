using ShopCayCanh.Library;
using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ShopCayCanh.Controllers.AuthControllerFacade
{
    public class SubMail
    {
        MailMessage mail_msg;

        public MailMessage CreateMailMessage(Muser item)
        {
            // email gửi đi và email nhận
            mail_msg = new MailMessage(Util.email, item.email);
            mail_msg.Subject = "Cấp lại mật khẩu Shop Cây Cảnh";
            mail_msg.Body = "Dear Mr." + item.fullname + "," +
                "Chúng tôi đã nhận được yêu cầu reset đổi mật khẩu của bạn, vui lòng tạo mới mật khẩu qua đường dẫn sau : http://localhost:22224/newPass/" + item.ID + "";
            mail_msg.IsBodyHtml = false;
            return mail_msg;
        }
    }
}
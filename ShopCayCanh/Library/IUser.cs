using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCayCanh.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ShopCayCanh.Library
{
    // ==============================================================
    //                     CONSTANT MESSAGE DEFINITION
    // ==============================================================
    public class UserStatusCode
    {
        public static readonly string ACCOUNT_EXIST = "Tên đăng nhập đã tồn tại";
        public static readonly string INVALID_PASSWORD = "Password không hợp lệ";
        public static readonly string INVALID_EMAIL = "Email không hợp lệ";
        public static readonly string INVALID_PHONE = "Số điện thoại không hợp lệ";
        public static readonly string INVALID_NAME = "Tên không hợp lệ";
        public static readonly string REGISTER_SUCCESSFULLY = "Tạo user thành công";
        public static readonly string EDIT_SUCCESSFULLY = "Cập nhật thành công";

    }

    // ==============================================================
    //                     PROXY PATTERN DEFINITION
    // ==============================================================
    internal interface IUser
    {
        string Register(ShopCayCanhDbContext context);
        string Edit(ShopCayCanhDbContext context);
    }

    // ==============================================================
    //                     PROXY OBJECT DEFINITION
    // ==============================================================
    class ProxyUser : IUser
    {
        Muser _user;

        public ProxyUser(Muser user)
        {
            _user = user;
        }

        public string Register(ShopCayCanhDbContext context)
        {
            if(!Is_Email_Valid(_user.email))
            {
                return UserStatusCode.INVALID_EMAIL;
            }
            if(!Is_Account_Not_Exist(_user.username, context))
            {
                return UserStatusCode.ACCOUNT_EXIST;
            }
            if(!Is_Password_Valid(_user.password))
            {
                return UserStatusCode.INVALID_PASSWORD;
            }
            if(!Is_Phone_Valid(_user.phone))
            {
                return UserStatusCode.INVALID_PHONE;
            }
            if(!Is_Name_Valid(_user.fullname))
            {
                return UserStatusCode.INVALID_NAME;
            }
            return _user.Register(context);
        }

        private bool Is_Password_Valid(string password)
        {
            if(password == "")
            {
                return false;
            }
            return true;
        }

        private bool Is_Email_Valid(string email_address)
        {
            try
            {
                var email = new MailAddress(email_address);
                return true;
            } 
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool Is_Account_Not_Exist(string account, ShopCayCanhDbContext context)
        {
            
            var Luser = context.users.Where(m => m.status == 1 && m.username == account);
            if (Luser.Count() > 0)
            {
                return false;
            }
            return true;
        }

        private bool Is_Phone_Valid(string phone)
        {
            string phone_format = @"^[0]{1}\d{9}$";
            Regex phone_reg = new Regex(phone_format);
            if (phone_reg.IsMatch(phone))
            {
                return true;
            }
            return false;
        }

        private bool Is_Name_Valid(string name)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] lines = System.IO.File.ReadAllLines(path + "/Library/invalid_name.txt");
            foreach(var invalid_name in lines)
            {
                if (name.ToLower().Contains(invalid_name.ToLower()))
                {
                    return false;
                }
            }           
            return true;
        }

        public string Edit(ShopCayCanhDbContext context)
        {
            if (!Is_Email_Valid(_user.email))
            {
                return UserStatusCode.INVALID_EMAIL;
            }
            if (!Is_Password_Valid(_user.password))
            {
                return UserStatusCode.INVALID_PASSWORD;
            }
            if (!Is_Phone_Valid(_user.phone))
            {
                return UserStatusCode.INVALID_PHONE;
            }
            if (!Is_Name_Valid(_user.fullname))
            {
                return UserStatusCode.INVALID_NAME;
            }
            return _user.Edit(context);
        }
    }
}

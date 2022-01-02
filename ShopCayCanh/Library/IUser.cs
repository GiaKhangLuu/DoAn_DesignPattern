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
        public static readonly string CHANGE_PASSWORD_SUCCESSFULLY = "Đổi mật khẩu thành công";
        public static readonly string WRONG_PASSWORD = "Mật khẩu không đúng";
        public static readonly string PASSWORD_AND_CF_PASSWORD_ARE_NOT_SIMILAR = "2 mật khẩu không khớp";
        public static readonly string RESET_SUCCESSFULLY = "Reset mật Khẩu thành công";
        public static readonly string TRY_AGAIN = "Vui lòng thử lại";
    }

    // ==============================================================
    //                     PROXY PATTERN DEFINITION
    // ==============================================================
    public interface IUser
    {
        string Register(ShopCayCanhDbContext context);
        string Edit(ShopCayCanhDbContext context);
        Task<string> ChangePassword(ShopCayCanhDbContext context, string oldPass, string rePass, string newPass);
        Task<string> ResetObliviousPassword(ShopCayCanhDbContext context, string rePass, string newPass);
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
            if(!_Is_Email_Valid(_user.email))
            {
                return UserStatusCode.INVALID_EMAIL;
            }
            if(!_Is_Account_Not_Exist(_user.username, context))
            {
                return UserStatusCode.ACCOUNT_EXIST;
            }
            if(!_Is_Password_Valid(_user.password))
            {
                return UserStatusCode.INVALID_PASSWORD;
            }
            if(!_Is_Phone_Valid(_user.phone))
            {
                return UserStatusCode.INVALID_PHONE;
            }
            if(!_Is_Name_Valid(_user.fullname))
            {
                return UserStatusCode.INVALID_NAME;
            }
            return _user.Register(context);
        }

        private bool _Is_Password_Valid(string password)
        {
            if(password == "")
            {
                return false;
            }
            return true;
        }

        private bool _Is_Email_Valid(string email_address)
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

        private bool _Is_Account_Not_Exist(string account, ShopCayCanhDbContext context)
        {
            
            var Luser = context.users.Where(m => m.status == 1 && m.username == account);
            if (Luser.Count() > 0)
            {
                return false;
            }
            return true;
        }

        private bool _Is_Phone_Valid(string phone)
        {
            string phone_format = @"^[0]{1}\d{9}$";
            Regex phone_reg = new Regex(phone_format);
            if (phone_reg.IsMatch(phone))
            {
                return true;
            }
            return false;
        }

        private bool _Is_Name_Valid(string name)
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
            if (!_Is_Email_Valid(_user.email))
            {
                return UserStatusCode.INVALID_EMAIL;
            }
            if (!_Is_Password_Valid(_user.password))
            {
                return UserStatusCode.INVALID_PASSWORD;
            }
            if (!_Is_Phone_Valid(_user.phone))
            {
                return UserStatusCode.INVALID_PHONE;
            }
            if (!_Is_Name_Valid(_user.fullname))
            {
                return UserStatusCode.INVALID_NAME;
            }
            return _user.Edit(context);
        }

        public async Task<string> ChangePassword(ShopCayCanhDbContext context, string oldPass, string rePass, string newPass)
        {
            
            if (!_user.password.Equals(oldPass))
            {
                return UserStatusCode.WRONG_PASSWORD;
            }
            else if (rePass != newPass)
            {
                return UserStatusCode.PASSWORD_AND_CF_PASSWORD_ARE_NOT_SIMILAR;
            }
            else
            {
                return (await _user.ChangePassword(context, oldPass, rePass, newPass));
            }
        }

        public async Task<string> ResetObliviousPassword(ShopCayCanhDbContext context, string rePass, string newPass)
        {
            if (rePass != newPass)
            {
                return UserStatusCode.PASSWORD_AND_CF_PASSWORD_ARE_NOT_SIMILAR;
            }
            else
            {
                return (await _user.ResetObliviousPassword(context, rePass, newPass));
            }
        }
    }
}

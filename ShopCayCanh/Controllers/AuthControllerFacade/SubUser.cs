using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Library;
using ShopCayCanh.Models;

namespace ShopCayCanh.Controllers.AuthControllerFacade
{
    public class SubUser
    {
        private IUser _proxy_user;

        public string Register(Muser muser, FormCollection fc, ShopCayCanhDbContext db)
        {
            string uname = fc["uname"];
            string fname = fc["fname"];
            string Pass = Mystring.ToMD5(fc["psw"]);
            string email = fc["email"];
            string phone = fc["phone"];

            muser.img = "defalt.png";
            muser.password = Pass;
            muser.username = uname;
            muser.fullname = fname;
            muser.email = email;
            muser.phone = phone;
            muser.gender = "nam";
            muser.access = 1;
            muser.created_at = DateTime.Now;
            muser.updated_at = DateTime.Now;
            muser.created_by = 1;
            muser.updated_by = 1;
            muser.status = 1;

            // Initialize PROXY object
            _proxy_user = new ProxyUser(muser);
            return _proxy_user.Register(db);
        }

        public async Task<string> ResetPassword(Muser muser, FormCollection fc, ShopCayCanhDbContext db)
        {
            string rePass = Mystring.ToMD5(fc["rePass"]);
            string newPass = Mystring.ToMD5(fc["password1"]);

            // Initialize PROXY object
            _proxy_user = new ProxyUser(muser);
            return (await _proxy_user.ResetObliviousPassword(db, rePass, newPass)); 
        }
    }
}
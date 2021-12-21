using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCayCanh.Controllers
{
    public class LienheController : Controller
    {
        // GET: Lienhe
        //ShopCayCanhDbContext db = new ShopCayCanhDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nhanlienket(Mcontact contact)
        {
            if (ModelState.IsValid)
            {
                contact.created_at = DateTime.Now;
                contact.updated_by = 1;
                contact.updated_at = DateTime.Now;
                contact.updated_by = 1;
                contact.status = 2;

                //db.Contacts.Add(contact);
                //db.SaveChanges();
                Singleton_Contact.GetInstance.Add(contact);

                Message.set_flash("Gửi liên hệ thành công", "success");

                return RedirectToAction("Index");
            }
            Message.set_flash("Gửi liên hệ thất bại", "danger");

            return View();
        }
    }
}
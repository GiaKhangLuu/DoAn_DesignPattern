using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Controllers.AuthControllerFacade
{
    public class SubContext
    {
        ShopCayCanhDbContext _context;

        public SubContext()
        {
            _context = new ShopCayCanhDbContext();
        }

        public ShopCayCanhDbContext Context { get => _context; set => _context = value; }

        public Muser FindById(int? id)
        {
            return _context.users.Find(id);
        }

        public Muser Find_Client_By_Username(string username)
        {
            return _context.users.FirstOrDefault(m => m.access == 1 && m.status == 1 & m.username == username);
        }
    }
}
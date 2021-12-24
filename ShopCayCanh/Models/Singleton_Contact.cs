using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Models
{
    public sealed class Singleton_Contact
    {
        
        public List<Mcontact> list_contact { get; } = new List<Mcontact>();
        private ShopCayCanhDbContext context = null;
        private static Singleton_Contact instance;

        public static Singleton_Contact GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton_Contact();
                }
                return instance;
            }          
        }

        private Singleton_Contact() {
            Init();
        }

        public void Init()
        {
            context = new ShopCayCanhDbContext();
            if (list_contact.Count == 0)
            {
                var contacts = context.Contacts.ToList();

                foreach (var item in contacts)
                {
                    list_contact.Add(item);
                }
            }
        }

        public Mcontact Find(int? value)
        {
            return context.Contacts.Find(value);
        }

        public void Add(Mcontact mcontact)
        {
            context.Contacts.Add(mcontact);
            context.SaveChanges();
            Refresh();
        }

        public void Remove(Mcontact mcontact)
        {
            context.Contacts.Remove(mcontact);
            context.SaveChanges();
            Refresh();
        }

        public void Refresh()
        {
            list_contact.Clear();          
            Init();
        }
    }
}
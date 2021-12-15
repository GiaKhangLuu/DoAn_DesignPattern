using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Models
{
    public sealed class Single_Contact
    {
        
        public List<Mcontact> list_contact { get; } = new List<Mcontact>();
        private ShopCayCanhDbContext context = null;
        private static Single_Contact instance;

        public static Single_Contact GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Single_Contact();
                }
                return instance;
            }          
        }


        private Single_Contact() {
            context = new ShopCayCanhDbContext();
            Init();
        }

        // only One time
        public void Init()
        {

            if (list_contact.Count == 0)
            {
                //var categories = context.Menus
                //    .Include(c => c.CategoryChildren)
                //    .AsEnumerable()
                //    .Where(c => c.ParentCategory == null)
                //    .ToList();

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

        //public void Update(AppDbContext context)
        //{
        //    listCatgegory.Clear();
        //    Init(context);
        //}

        public void Add(Mcontact mcontact)
        {
            context.Contacts.Add(mcontact);
            context.SaveChanges();

            list_contact.Clear();

            Init();
        }

        public void Remove(Mcontact mcontact)
        {
            context.Contacts.Remove(mcontact);
            context.SaveChanges();

            list_contact.Clear();

            Init();
        }
    }
}
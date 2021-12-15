using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Models
{
    public sealed class Singleton_Menu
    {
        
        public List<Mmenu> list_menu { get; } = new List<Mmenu>();
        private ShopCayCanhDbContext context = null;
        private static Singleton_Menu instance;

        public static Singleton_Menu GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton_Menu();
                }
                return instance;
            }          
        }


        private Singleton_Menu() {
            context = new ShopCayCanhDbContext();
            Init();
        }

        // only One time
        public void Init()
        {

            if (list_menu.Count == 0)
            {
                //var categories = context.Menus
                //    .Include(c => c.CategoryChildren)
                //    .AsEnumerable()
                //    .Where(c => c.ParentCategory == null)
                //    .ToList();

                var menus = context.Menus.ToList();

                foreach (var item in menus)
                {
                    list_menu.Add(item);
                }
            }
        }

        public Mmenu Find(int? value)
        {
            return context.Menus.Find(value);
        }

        //public void Update(AppDbContext context)
        //{
        //    listCatgegory.Clear();
        //    Init(context);
        //}

        public void Add(Mmenu mmenu)
        {
            context.Menus.Add(mmenu);
            context.SaveChanges();

            list_menu.Clear();

            Init();
        }

        public void Remove(Mmenu mmenu)
        {
            context.Menus.Remove(mmenu);
            context.SaveChanges();

            list_menu.Clear();

            Init();
        }
    }
}
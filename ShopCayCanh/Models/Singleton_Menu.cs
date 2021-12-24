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
            Init();
        }

        public void Init()
        {
            context = new ShopCayCanhDbContext();
            if (list_menu.Count == 0)
            {
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

        public void Add(Mmenu mmenu)
        {
            context.Menus.Add(mmenu);
            context.SaveChanges();
            Refresh();
        }

        public void Remove(Mmenu mmenu)
        {
            context.Menus.Remove(mmenu);
            context.SaveChanges();
            Refresh();         
        }

        public void Refresh()
        {
            list_menu.Clear();
            Init();
        }
    }
}
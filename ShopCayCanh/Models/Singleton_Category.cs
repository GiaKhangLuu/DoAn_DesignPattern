using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Models
{
    public sealed class Singleton_Category
    {
        
        public List<Mcategory> list_cat { get; } = new List<Mcategory>();
        private ShopCayCanhDbContext context = null;
        private static Singleton_Category instance;

        public static Singleton_Category GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton_Category();
                }
                return instance;
            }          
        }


        private Singleton_Category() {
            Init();
        }

        public void Init()
        {

            if (list_cat.Count == 0)
            {
                context = new ShopCayCanhDbContext();
                var cats = context.Categorys.ToList();

                foreach (var item in cats)
                {
                    list_cat.Add(item);
                }
            }
        }

        public Mcategory Find(int? value)
        {
            return context.Categorys.Find(value);
        }

        public void Add(Mcategory mcategory)
        {
            context.Categorys.Add(mcategory);
            context.SaveChanges();
            Refresh();
        }

        public void Remove(Mcategory mcategory)
        {
            context.Categorys.Remove(mcategory);
            context.SaveChanges();
            Refresh(); 
        }

        public void Refresh()
        {
            list_cat.Clear();
            Init();
        }
    }
}
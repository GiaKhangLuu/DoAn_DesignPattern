using System;
using System.Collections.Generic;
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
            context = new ShopCayCanhDbContext();
            Init();
        }

        // only One time
        public void Init()
        {

            if (list_cat.Count == 0)
            {
                //var categories = context.Menus
                //    .Include(c => c.CategoryChildren)
                //    .AsEnumerable()
                //    .Where(c => c.ParentCategory == null)
                //    .ToList();

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

        //public void update(appdbcontext context)
        //{
        //    listcatgegory.clear();
        //    init(context);
        //}

        public void Add(Mcategory mcategory)
        {
            context.Categorys.Add(mcategory);
            context.SaveChanges();

            list_cat.Clear();

            Init();
        }

        public void Remove(Mcategory mcategory)
        {
            context.Categorys.Remove(mcategory);
            context.SaveChanges();

            list_cat.Clear();

            Init();
        }
    }
}
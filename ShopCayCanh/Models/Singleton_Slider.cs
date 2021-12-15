using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Models
{
    public sealed class Single_Slider
    {
        
        public List<Mslider> list_slider { get; } = new List<Mslider>();
        private ShopCayCanhDbContext context = null;
        private static Single_Slider instance;

        public static Single_Slider GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Single_Slider();
                }
                return instance;
            }          
        }


        private Single_Slider() {
            context = new ShopCayCanhDbContext();
            Init();
        }

        // only One time
        public void Init()
        {

            if (list_slider.Count == 0)
            {
                //var categories = context.Menus
                //    .Include(c => c.CategoryChildren)
                //    .AsEnumerable()
                //    .Where(c => c.ParentCategory == null)
                //    .ToList();

                var sliders = context.Sliders.ToList();

                foreach (var item in sliders)
                {
                    list_slider.Add(item);
                }
            }
        }

        public Mslider Find(int? value)
        {
            return context.Sliders.Find(value);
        }

        //public void Update(AppDbContext context)
        //{
        //    listCatgegory.Clear();
        //    Init(context);
        //}

        public void Add(Mslider mslider)
        {
            context.Sliders.Add(mslider);
            context.SaveChanges();

            list_slider.Clear();

            Init();
        }

        public void Remove(Mslider mslider)
        {
            context.Sliders.Remove(mslider);
            context.SaveChanges();

            list_slider.Clear();

            Init();
        }
    }
}
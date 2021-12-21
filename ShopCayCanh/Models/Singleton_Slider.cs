using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Models
{
    public sealed class Singleton_Slider
    {
        
        public List<Mslider> list_slider { get; } = new List<Mslider>();
        private ShopCayCanhDbContext context = null;
        private static Singleton_Slider instance;

        public static Singleton_Slider GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton_Slider();
                }
                return instance;
            }          
        }


        private Singleton_Slider() {
            Init();
        }

        // only One time
        public void Init()
        {
            context = new ShopCayCanhDbContext();
            if (list_slider.Count == 0)
            {
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
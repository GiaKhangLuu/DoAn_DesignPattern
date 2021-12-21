using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Models
{
    public sealed class Singleton_Topic
    {
        
        public List<Mtopic> list_topic { get; } = new List<Mtopic>();
        private ShopCayCanhDbContext context = null;
        private static Singleton_Topic instance;

        public static Singleton_Topic GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton_Topic();
                }
                return instance;
            }          
        }

        private Singleton_Topic() {
            Init();
        }

        public void Init()
        {
            context = new ShopCayCanhDbContext();
            if (list_topic.Count == 0)
            {
                var contacts = context.topics.ToList();

                foreach (var item in contacts)
                {
                    list_topic.Add(item);
                }
            }
        }

        public Mtopic Find(int? value)
        {
            return context.topics.Find(value);
        }

        public void Add(Mtopic mtopic)
        {
            context.topics.Add(mtopic);
            context.SaveChanges();
            Refresh();
        }

        public void Remove(Mtopic mtopic)
        {
            context.topics.Remove(mtopic);
            context.SaveChanges();
            Refresh();
        }

        public void Refresh()
        {
            list_topic.Clear();
            Init();
        }
    }
}
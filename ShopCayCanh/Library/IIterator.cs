using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCayCanh.Models;

namespace ShopCayCanh.Library
{
    internal interface IIterator<T>
    {
        T First();
        T Next();
        bool IsDone { get;  }
        T CurrentItem { get; }

    }

    public class OrderIterator : IIterator<Morder>
    {
        List<Morder> _list_order;
        int current = 0;
        int step = 1;

        public OrderIterator(List<Morder> list_order)
        {
            _list_order = list_order;
        }

        public bool IsDone
        {
            get { return current >= _list_order.Count; }
        }

        public Morder CurrentItem => _list_order[current];

        public Morder First()
        {
            current = 0;
            if (_list_order.Count > 0)
            {
                return _list_order[current];
            }
            return null;
        }

        public Morder Next()
        {
            current += step;
            if(!IsDone)
            {
                return _list_order[current];
            }
            else
            {
                return null;
            }
        }
    }

    public class StringIterator : IIterator<String>
    {
        List<String> _list_item;
        int current = 0;
        int step = 1;

        public StringIterator(List<String> list_item)
        {
            _list_item = list_item;
        }

        public bool IsDone
        {
            get { return current >= _list_item.Count; }
        }

        public String CurrentItem => _list_item[current];

        public String First()
        {
            current = 0;
            if (_list_item.Count > 0)
            {
                return _list_item[current];
            }
            return null;
        }

        public String Next()
        {
            current += step;
            if (!IsDone)
            {
                return _list_item[current];
            }
            else
            {
                return null;
            }
        }
    }
}

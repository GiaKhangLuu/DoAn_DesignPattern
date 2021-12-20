using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCayCanh.Models
{
    public interface IPrototype
    {
        IPrototype Clone();
    }
}

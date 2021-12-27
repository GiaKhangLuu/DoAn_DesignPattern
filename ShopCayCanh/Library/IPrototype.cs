using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCayCanh.Library
{
    /* 
    ===================================================================================
                                 PROTOTYPE PATTERN DEFINITION
    ===================================================================================
    */
    public interface IPrototype
    {
        IPrototype Clone();
    }
}

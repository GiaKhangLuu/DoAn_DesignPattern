using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCayCanh.Models;

namespace ShopCayCanh.Library.Strategy
{
    public interface IFavoriteProduct
    {
        object AddItem(long productID, List<MfavoriteProduct> list);
    }
}

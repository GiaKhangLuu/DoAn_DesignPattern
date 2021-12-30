using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCayCanh.Models;

namespace ShopCayCanh.Library.Strategy
{
    public interface ICartStrategy
    {
        object AddToCart(long productID, int quantity, Mproduct product, List<Cart_item> list = null);
    }
}

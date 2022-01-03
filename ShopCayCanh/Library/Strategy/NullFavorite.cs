using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Library.Strategy
{
    public class NullFavorite : IFavoriteProduct
    {
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        public object AddItem(long productID, List<MfavoriteProduct> list)
        {
            var item = new MfavoriteProduct();
            Mproduct product = db.Products.Find(productID);
            item.favoriteProduct = product;
            item.status = 3;
            item.method = "favoriteEmpty";
            list.Add(item);
            return item;
        }
    }
}
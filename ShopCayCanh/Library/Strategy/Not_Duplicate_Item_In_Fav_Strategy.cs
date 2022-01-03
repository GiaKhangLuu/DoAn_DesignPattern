using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Library.Strategy
{
    public class Not_Duplicate_Item_In_Fav_Strategy : IFavoriteProduct
    {
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        public object AddItem(long productID, List<MfavoriteProduct> list)
        {
            var item = new MfavoriteProduct();
            Mproduct product = db.Products.Find(productID);
            item.favoriteProduct = product;
            item.status = 2;            
            item.method = "favoriteExist";
            list.Add(item);
            return item;
        }
    }
}
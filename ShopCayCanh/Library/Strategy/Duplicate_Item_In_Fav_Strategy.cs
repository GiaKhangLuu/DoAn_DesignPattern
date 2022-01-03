using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Library.Strategy
{
    public class Duplicate_Item_In_Fav_Strategy : IFavoriteProduct
    {
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();

        public object AddItem(long productID, List<MfavoriteProduct> list)
        {
            Mproduct product = db.Products.Find(productID);

            return new { status = 1, meThod = "ExistProduct" };
        }
    }
}
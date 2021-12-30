using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Library.Strategy
{
    public class NullCartStrategy : ICartStrategy
    {
        public object AddToCart(long productID, int quantity, Mproduct product, List<Cart_item> list)
        {
            var item = new Cart_item();
            item.product = product;
            item.meThod = "cartEmpty";
            item.countCart = 1;
            item.f = false;
            if (quantity > (product.number - product.sold))
            {
                item.f = true;
                item.quantity = 0;
            }
            else
            {
                item.quantity = quantity;
                list.Add(item);
                if (item.product.pricesale > 0)
                {
                    item.priceTotal = (((int)item.product.price) - ((int)item.product.price / 100 * (int)item.product.pricesale)) * ((int)item.quantity);
                }
                else
                {
                    item.priceTotal = (int)product.price;
                }
                item.priceTotal = (((int)item.product.price) - ((int)item.product.price / 100 * (int)item.product.pricesale)) * ((int)item.quantity);
            }
            return item;
        }
    }
}
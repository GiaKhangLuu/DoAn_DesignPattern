using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Library.Strategy
{
    public class Not_Duplicate_Item_In_Cart_Strategy : ICartStrategy
    {
        public object AddToCart(long productID, int quantity, Mproduct product, List<Cart_item> list)
        {
            var item = new Cart_item();
            item.meThod = "cartExist";
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
                item.product = product;
                item.countCart = list.Count();
                item.meThod = "cartExist";
                int priceTotol = 0;
                IIterator<Cart_item> iterator = new CartItemIterator(list);
                var item1 = iterator.First();
                while (!iterator.IsDone)
                {
                    if (item1.product.pricesale > 0)
                    {
                        int temp = (((int)item1.product.price) - ((int)item1.product.price / 100 * (int)item1.product.pricesale)) * ((int)item1.quantity);
                        priceTotol += temp;
                    }
                    else
                    {
                        int temp = (int)item1.product.price * (int)item1.quantity;
                        priceTotol += temp;
                    }
                    item1 = iterator.Next();
                }

                item.priceTotal = priceTotol;
                item.priceSaleee = (int)product.price - (int)product.price / 100 * (int)product.pricesale;
            }

            return item;
        }
    }
}
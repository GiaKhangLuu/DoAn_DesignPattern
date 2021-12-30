using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Library.Strategy
{
    public class Duplicate_Item_In_Cart_Strategy : ICartStrategy
    {
        public object AddToCart(long productID, int quantity, Mproduct product, List<Cart_item> list)
        {
            int quantity1 = 0;
            bool bad = false;
            int priceTotol = 0;

            IIterator<Cart_item> iterator = new CartItemIterator(list);
            var item1 = iterator.First();
            while (!iterator.IsDone)
            {
                // Check quantity
                if (item1.product.ID == productID)
                {
                    if ((item1.quantity + quantity) > (item1.product.number - item1.product.sold))
                    {
                        bad = true;
                    }
                    else
                    {
                        item1.quantity += quantity;
                        quantity1 = item1.quantity;
                    }
                }

                // Compute price total
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

            return new
            {
                ProductPrice = ((int)product.price) - (((int)product.price / 100) * ((int)product.pricesale)),
                bad = bad,
                price = product.price,
                priceSale = product.pricesale,
                quantity = quantity1,
                priceTotol = priceTotol,
                productID = productID,
                meThod = "updateQuantity"
            };
        }
    }
}
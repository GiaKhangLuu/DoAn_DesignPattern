using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Library;
using ShopCayCanh.Library.Strategy;

namespace ShopCayCanh.Controllers
{
    public class CartController : Controller
    {
        // khởi tạo session:
        private const string SessionCart = "SessionCart";
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();
        ICartStrategy cart_strategy;
        

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            return View(list);
        }

        public ActionResult card_header()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            float priceTotol = 0;
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
                IIterator<Cart_item> iterator = new CartItemIterator(list);
                var item = iterator.First();
                while (!iterator.IsDone)
                {
                    if (item.product.pricesale > 0)
                    {
                        int temp = (((int)item.product.price) - ((int)item.product.price / 100 * (int)item.product.pricesale)) * ((int)item.quantity);
                        priceTotol += temp;
                    }
                    else
                    {
                        int temp = (int)item.product.price * (int)item.quantity;
                        priceTotol += temp;
                    }

                    item = iterator.Next();
                }

            }
            ViewBag.CartTotal = priceTotol;
            return View(list);
        }

        public RedirectToRouteResult updateitem(long P_SanPhamID, int P_quantity)
        {
            var cart = Session[SessionCart];
            var list = (List<Cart_item>)cart;
            Cart_item itemSua = list.FirstOrDefault(m => m.product.ID == P_SanPhamID);
            if (itemSua != null)
            {
                if (itemSua.quantity + P_quantity > itemSua.product.number - itemSua.product.sold) {
                    Message.set_flash("Số lượng bạn chọn đã lớn hơn số lượng trong kho", "danger");
                    return RedirectToAction("Index");
                }
                itemSua.quantity = P_quantity;
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult deleteitem(long productID)
        {
            var cart = Session[SessionCart];
            var list = (List<Cart_item>)cart;

            Cart_item itemXoa = list.FirstOrDefault(m => m.product.ID == productID);
            if (itemXoa != null)
            {
                list.Remove(itemXoa);
                if (list.Count == 0)
                {
                    Session["SessionCart"] = null;
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult Additem(long productID, int quantity)
        {         
            Mproduct product = db.Products.Find(productID);
            var cart = Session[SessionCart];

            // cart is not null
            if (cart != null)
            {
                var list = (List<Cart_item>)cart;
                // item has been added to cart
                if (list.Exists(m => m.product.ID == productID))
                {
                    cart_strategy = new Duplicate_Item_In_Cart_Strategy();             
                }
                // item has not been added to cart
                else
                {
                    cart_strategy = new Not_Duplicate_Item_In_Cart_Strategy();
                }
                var item = cart_strategy.AddToCart(productID, quantity, product, list);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            // cart is null
            else
            {
                var list = new List<Cart_item>();
                cart_strategy = new NullCartStrategy();
                var item = (Cart_item) cart_strategy.AddToCart(productID, quantity, product, list);
                if(!item.f && item.quantity != 0)
                {
                    Session[SessionCart] = list;
                }
                return Json(item, JsonRequestBehavior.AllowGet);
            }                  
        }
    }
}
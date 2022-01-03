using ShopCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCayCanh.Library.Strategy;

namespace ShopCayCanh.Controllers
{
    public class FavoriteProductController : Controller
    {
        private const string SessionFavorite = "favorite";
        ShopCayCanhDbContext db = new ShopCayCanhDbContext();
        IFavoriteProduct favorite_strategy;

        // GET: FavoriteProduct
        public ActionResult favoriteList()
        {
            var favorite = Session[SessionFavorite];
            var list = new List<MfavoriteProduct>();
            if (favorite != null)
            {
                list = (List<MfavoriteProduct>)favorite;
            }
            return View("_listFavorite", list);
        }

        public JsonResult Additem(long productID)
        {
            var favorite = Session[SessionFavorite];

            // favorite is not null
            if (favorite != null)
            {
                var list = (List<MfavoriteProduct>)favorite;
                // product existed in favorite list
                if (list.Exists(m => m.favoriteProduct.ID == productID))
                {
                    favorite_strategy = new Duplicate_Item_In_Fav_Strategy();
                }
                // product didnt exist in favorite list
                else
                {
                    favorite_strategy = new Not_Duplicate_Item_In_Fav_Strategy();
                }
                var item = favorite_strategy.AddItem(productID, list);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            // favorite is null
            else
            {
                var list = new List<MfavoriteProduct>();
                favorite_strategy = new NullFavorite();
                var item = (MfavoriteProduct)favorite_strategy.AddItem(productID, list);
                Session[SessionFavorite] = list;
                return Json(item, JsonRequestBehavior.AllowGet);

            }
        }       
    }
}
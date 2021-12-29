using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopCayCanh.Library
{
    public class SiteFactory
    {
        // ====================================================================
        //          IMPLEMENTATION OF FACTORY - USED TO CREATE OBJECTS
        // ====================================================================

        public AbstractSite CreateSite(SiteName site_name, string slug_name, int slug_id)
        {
            switch (site_name)
            {
                case SiteName.Category:
                    return new CategorySite(slug_name, slug_id);
                case SiteName.Error:
                    return new ErrorSite();
                case SiteName.Home:
                    return new HomeSite();
                case SiteName.Topic:
                    return new TopicSite(slug_name, slug_id);
                case SiteName.PostDetail:
                    return new PostDetailSite(slug_name, slug_id);
                case SiteName.ProductDetail:
                    return new ProductDetailSite(slug_name, slug_id);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
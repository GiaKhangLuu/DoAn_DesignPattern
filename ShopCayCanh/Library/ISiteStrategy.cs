using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShopCayCanh.Models;

namespace ShopCayCanh.Library
{
    /* 
    ===================================================================================
                            STRATEGY PATTERN DEFINITION
    ===================================================================================
    */
    interface ISiteStrategy
    {
        ActionResult Go_To_Site(string slug, int id);
    }
}

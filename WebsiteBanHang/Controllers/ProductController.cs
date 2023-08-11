using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers 
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Product
        public ActionResult Detail(int ID)
        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n =>n.ID == ID).FirstOrDefault();
            return View(objProduct);
        }

    }
}
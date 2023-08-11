using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers { 
    public class CategoryController : Controller
    { WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objWebsiteBanHangEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int ID)
        {
            var listProduct = objWebsiteBanHangEntities.Products.Where(n =>n.CategoryId == ID).ToList();
            return View(listProduct);
        }
    }
}
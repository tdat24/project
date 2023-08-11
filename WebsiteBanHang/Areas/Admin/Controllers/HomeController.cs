using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Controllers;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
      
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = db.Categories.OrderByDescending(n => n.ID).ToList();
            objHomeModel.ListProduct = db.Products.ToList();
            return View(objHomeModel);  
        }
        [HttpGet]
        public ActionResult Register() { 
        
        return View();
        
        }
        public ActionResult NotCredential()
        {
            return View();
        }
    }
}
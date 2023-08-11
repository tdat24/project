using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Controllers;
using static WebsiteBanHang.Common;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Product
        [HasCredentialAtrribute(RoleCode = "view-add-edit-delete")]
        public ActionResult Index(string currentFilter,string SearchString,int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objWebsiteBanHangEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct= objWebsiteBanHangEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending( n => n.ID).ToList();

            return View(lstProduct.ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
 
        [HasCredentialAtrribute(RoleCode = "view-add-edit-delete")]
        public ActionResult Create()
        {
            Common objCommon = new Common();

            var lstCat = objWebsiteBanHangEntities.Categories.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory,"Id","Name");

            var lstBrand = objWebsiteBanHangEntities.Brands.ToList();
            DataTable dtBrand =  converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");
            return View();
        }
        [HttpPost]
       
        public ActionResult Create(Product objProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase Avatar = Request.Files[0];
                    if(Avatar != null && Avatar.ContentLength > 0)
                    {
                        objProduct.Avatar = Avatar.FileName;
                        Avatar.SaveAs(Server.MapPath("/Content/images/items/" + Avatar.FileName));
                    }
                    objWebsiteBanHangEntities.Products.Add(objProduct);
                    objWebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index","Product", new {area = "Admin"});
                }

                catch 

                {
                    return View();
                }
            }
            return View(objProduct);                   
        }
        [HasCredentialAtrribute(RoleCode = "view-add-edit-delete")]
        public ActionResult Edit(int? ID)
        {
            Product item = new Product();
            if (ID != null)
            {
                item = objWebsiteBanHangEntities.Products.Find(ID);
            }
            var brands = new SelectList(objWebsiteBanHangEntities.Brands.ToList(),"ID","Name");
            ViewBag.Brands = brands;
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(Product model)
        {
            //tìm đối tượng
            var updateModel = objWebsiteBanHangEntities.Products.Find(model.ID);
            //gán giá trị
            updateModel.Name = model.Name;
            updateModel.Avatar = model.Avatar;
            updateModel.TypeId = model.TypeId;
            updateModel.CategoryId = model.CategoryId;
            updateModel.BrandId = model.BrandId;
            updateModel.Price = model.Price;
            HttpPostedFileBase Avatar = Request.Files[0];
            if (Avatar != null && Avatar.ContentLength > 0)
            {
                updateModel.Avatar = Avatar.FileName;
                Avatar.SaveAs(Server.MapPath("/Content/images/items/" + Avatar.FileName));
            }

            //lưu thay đổi

            objWebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HasCredentialAtrribute(RoleCode = "view-add-edit-delete")]
        public ActionResult Delete(int ID)
        {// tìm 
            var updatemodel = objWebsiteBanHangEntities.Products.Find(ID);
            //xóa
            objWebsiteBanHangEntities.Products.Remove(updatemodel);

            objWebsiteBanHangEntities.SaveChanges();

            return RedirectToAction("Index");

        }
        public int getProduct()
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();
            return db.Products.Count();

        }
        


    }
}
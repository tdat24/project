using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        [HasCredentialAtrribute(RoleCode = "view-add-edit-delete")]
        public ActionResult Index()
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            List<Brand> danhsachbrand = db.Brands.ToList();
            return View(danhsachbrand);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();
            var check = db.Brands.FirstOrDefault(s => s.Name == model.Name);
            if (check == null)
            {

                db.Brands.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "tựa đề đã tồn tại");
                return View();
            }


        }

        public ActionResult Edit(int ID)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();
            Brand Brand = db.Brands.Find(ID);
            return View(Brand);
        }
        [HttpPost]
        public ActionResult Edit(Brand model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Brands.Find(model.ID);

            updateModel.Name = model.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Brands.Find(ID);
            db.Brands.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
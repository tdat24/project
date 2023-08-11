using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        [HasCredentialAtrribute(RoleCode = "view-add-edit-delete")]
        public ActionResult Index()
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            List<Category> danhsachcategory = db.Categories.ToList();
            return View(danhsachcategory);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();
            var check = db.Categories.FirstOrDefault(s => s.Name == model.Name);
            if (check == null)
            {
                
                db.Categories.Add(model);
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
            Category category = db.Categories.Find(ID);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Categories.Find(model.ID);

            updateModel.Name = model.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Categories.Find(ID);
            db.Categories.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
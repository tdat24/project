using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Controllers;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class GroupController : BaseController
    {
        // GET: Admin/Group

        public ActionResult Index()
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            List<Group> danhsachgroup = db.Groups.ToList();
            return View(danhsachgroup);
        }


        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Group model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();
            var check = db.Groups.FirstOrDefault(s => s.Name == model.Name);
            if (check == null)
            {

                
                db.Groups.Add(model);
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
            Group Group = db.Groups.Find(ID);
            return View(Group);
        }
        [HttpPost]
        public ActionResult Edit(Group model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Groups.Find(model.ID);

            updateModel.ID = model.ID;
            updateModel.Name = model.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int ID)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Groups.Find(ID);
            db.Groups.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        
         

        }
    }

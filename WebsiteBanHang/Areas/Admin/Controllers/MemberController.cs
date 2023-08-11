using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Controllers;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class MemberController : BaseController
    {
        // GET: Admin/Member
        [HasCredentialAtrribute(RoleCode = "view-add-edit-delete")]
        public ActionResult Index()
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            List<Member> danhsachmember = db.Members.ToList();
            return View(danhsachmember);
            
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Member model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();
            var check = db.Members.FirstOrDefault(s => s.LoginName == model.LoginName);
            if (check == null)
            {
                HttpPostedFileBase Avatar = Request.Files[0];
                if (Avatar != null && Avatar.ContentLength > 0)
                {
                    model.Avatar = Avatar.FileName;
                    Avatar.SaveAs(Server.MapPath("/Content/images/items/" + Avatar.FileName));
                }
                model.Password = GetMD5(model.Password);
                db.Members.Add(model);
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
            Member member = db.Members.Find(ID);
            return View(member);
        }
        [HttpPost]
        public ActionResult Edit(Member model)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Members.Find(model.ID);


            updateModel.ID = model.ID;
            updateModel.Name = model.Name;
            updateModel.Avatar = model.Avatar;
            updateModel.LoginName = model.LoginName;
            updateModel.Password = GetMD5(model.Password);
            updateModel.CreatedDate = model.CreatedDate;
            updateModel.GroupId = model.GroupId;
            HttpPostedFileBase Avatar = Request.Files[0];
            if (Avatar != null && Avatar.ContentLength > 0)
            {
                updateModel.Avatar = Avatar.FileName;
                Avatar.SaveAs(Server.MapPath("/Content/images/members/" + Avatar.FileName));
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();

            var updateModel = db.Members.Find(ID);
            db.Members.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("Index");


        }


        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]

        public ActionResult CheckMember(FormCollection form)
        {

            string LoginName = form["LoginName"], Password = GetMD5Hash(form["Password"]);
            var item = db.Members.Where(m => m.LoginName == LoginName && m.Password == Password).FirstOrDefault();
            if (item == null)
            {   ViewBag.error = "Login failed";
                    TempData["error"] = " tài khoản đăng nhập sai";
                return RedirectToAction("Login","Member");
            }
            var RoleIdofMember = db.Credentials.Where(c => c.GroupId == item.GroupId).Select(c => c.RoleId).ToList();
            var RoleCodeList = db.Roles.Where(r => RoleIdofMember.Contains(r.Id)).Select(r => r.Code).ToList();
            Session["Credential"] = RoleCodeList;
            Session["Member"] = item;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session["Member"] = null;
            return RedirectToAction("Login","Member");
        }

        public static string GetMD5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < (data.Length); i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));

                }
                return sBuilder.ToString();
            }
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public int getMember()
        {
            WebsiteBanHangEntities db = new WebsiteBanHangEntities();
            return db.Members.Count();

        }
    }
}
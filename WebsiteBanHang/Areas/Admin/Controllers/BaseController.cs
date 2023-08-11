using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebsiteBanHang.Context; //using cai nay 
using WebsiteBanHang.Models;
using Member = WebsiteBanHang.Context.Member;

namespace WebsiteBanHang.Controllers
{
    public class BaseController : Controller
    {
        public WebsiteBanHangEntities db = new WebsiteBanHangEntities(); //khai bao db
        public Member member;
        protected override void OnActionExecuting(ActionExecutingContext filterContext) 
        {
            if (Session["Member"] == null && filterContext.RouteData.Values["controller"].ToString() != "Member") 
            {
                filterContext.Result = new RedirectResult("/Admin/Member/Login");
            }
            else
            {
                member = (Member)Session["Member"];
            }
            base.OnActionExecuting(filterContext);
        }


        
    }
}
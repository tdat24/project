using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Payment
        public ActionResult Index(string shipName, string mobile, string address, string email)
        {
            if (Session["idUser"] == null) 
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                if (lstCart != null && lstCart.Count > 0)
                {
                    Order objOrder = new Order();
                    objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    objOrder.UserId = int.Parse(Session["idUser"].ToString());
                    objOrder.CreatedOnUtc = DateTime.Now;
                    objOrder.Status = 1;
                    objOrder.ShipName = shipName;
                    objOrder.ShipMobile = mobile;
                    objOrder.ShipAddress = address;
                    objOrder.ShipEmail = email;
                    objWebsiteBanHangEntities.Orders.Add(objOrder);
                    objWebsiteBanHangEntities.SaveChanges();

                    int intOrderID = objOrder.ID;

                    List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                    foreach (var item in lstCart)
                    {
                        OrderDetail obj = new OrderDetail();
                        obj.Quantity = item.Quantity;
                        obj.OrderId = intOrderID;
                        obj.ProductId = item.Product.ID;
                        lstOrderDetail.Add(obj);
                    }
                    objWebsiteBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                    objWebsiteBanHangEntities.SaveChanges();
                }
                else
                {
                    TempData["Error"] = "Không có sản phẩm trong giỏ hàng.";
                    return RedirectToAction("Home", "Index");
                    // Không có sản phẩm trong giỏ hàng, xử lý lỗi hoặc hiển thị thông báo tùy ý.
                    // Ví dụ: TempData["Error"] = "Không có sản phẩm trong giỏ hàng.";
                    // Hoặc: ModelState.AddModelError("", "Không có sản phẩm trong giỏ hàng.");
                }
            }
            return View();
        }

    }
}
using PosEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PosEcommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart

            [HttpPost]
        public ActionResult Add(ItemModel item)
        {
            CategoryController cc = new CategoryController();

            List<ItemTransferModel> li;

            if (Session["cart"] == null)
            {
                li = new List<ItemTransferModel>();

                Session["count"] = 1;

            }
            else
            {
                li = (List<ItemTransferModel>)Session["cart"];
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }



            decimal basicPrice = 0;
            decimal price = 0;

            int offerId = 0;
            string discountType = "1";
            decimal discountValue = 0;

            basicPrice = (decimal)item.ItemUnitList.FirstOrDefault().price;
            if (item.ItemUnitList.FirstOrDefault().offerId != null && item.ItemUnitList.FirstOrDefault().offerId != 0)
            {
                offerId = (int)item.ItemUnitList.FirstOrDefault().offerId;
                discountType = item.ItemUnitList.FirstOrDefault().discountType;
                discountValue = (decimal)item.ItemUnitList.FirstOrDefault().discountValue;

                price = cc.GetdiscountPrice(item.ItemUnitList.FirstOrDefault().discountType, item.ItemUnitList.FirstOrDefault().discountValue, item.ItemUnitList.FirstOrDefault().price);
            }
            else
                price = basicPrice;



            li.Add(new ItemTransferModel()
            {
                itemId = item.itemId,
                itemUnitId = item.ItemUnitList.FirstOrDefault().itemUnitId,
                price = price,
                itemUnitPrice = basicPrice,
                quantity = 1,
                offerId = offerId,
                offerValue = discountValue,
                offerType = decimal.Parse(discountType),
            });

            Session["cart"] = li;
            ViewBag.cartCount = li.Count();

            JsonResult result = this.Json(new
            {
                cartCount = li.Count(),              
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}
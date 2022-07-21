using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;

namespace PosEcommerce.Controllers
{
    public class CartController : Controller
    {

        public async Task<ActionResult> CartView()
        {
            try
            { 
            


                return View();
            }
            catch(Exception ex)
            {
               return View( "not found");
            }
          
        }

        [HttpPost]
        public ActionResult Add(ItemModel item)
        {
            CategoryController cc = new CategoryController();

            List<ItemTransferModel> li;
            ItemTransferModel itemFound=null;

            if (Session["cart"] == null)
            {
                li = new List<ItemTransferModel>();

                Session["count"] = 1;

            }
            else
            {
                li = (List<ItemTransferModel>)Session["cart"];

                itemFound = li.Where(x => x.itemUnitId == item.ItemUnitList.FirstOrDefault().itemUnitId).FirstOrDefault();
                if(itemFound == null)
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }


            if (itemFound == null)
            {
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
            }
            else
                itemFound.quantity++;

            Session["cart"] = li;

            JsonResult result = this.Json(new
            {
                cartCount = li.Count(),
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public ActionResult AddWithQuantity(ItemModel item, string quantity)
        {
            CategoryController cc = new CategoryController();

            List<ItemTransferModel> li;
            ItemTransferModel itemFound=null;

            if (Session["cart"] == null)
            {
                li = new List<ItemTransferModel>();

                Session["count"] = 1;

            }
            else
            {
                li = (List<ItemTransferModel>)Session["cart"];

                itemFound = li.Where(x => x.itemUnitId == item.ItemUnitList.FirstOrDefault().itemUnitId).FirstOrDefault();
                if(itemFound == null)
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }


            if (itemFound == null)
            {
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
                    quantity = int.Parse(quantity),
                    offerId = offerId,
                    offerValue = discountValue,
                    offerType = decimal.Parse(discountType),
<<<<<<< Updated upstream
                });
            }
            else
                itemFound.quantity = int.Parse(quantity);
=======
                    image = item.image,
                });
            }
            else
                itemFound.quantity += int.Parse(quantity);
>>>>>>> Stashed changes

            Session["cart"] = li;

            JsonResult result = this.Json(new
            {
                cartCount = li.Count(),
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
<<<<<<< Updated upstream
=======

         [HttpGet]
        public ActionResult ViewCartItems()
        {
            CategoryController cc = new CategoryController();

            List<ItemTransferModel> li;

            if (Session["cart"] == null)
            {
                li = new List<ItemTransferModel>();

            }
            else
            {
                li = (List<ItemTransferModel>)Session["cart"];
              
            }


            JsonResult result = this.Json(new
            {
                cartItems = li,
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
>>>>>>> Stashed changes



    }
}
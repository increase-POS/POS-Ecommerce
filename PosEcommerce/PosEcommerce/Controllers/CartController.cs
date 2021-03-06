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
                List<ItemTransferModel> li;

                if (Session["cart"] == null)
                {
                    li = new List<ItemTransferModel>();

                }
                else
                {
                    li = (List<ItemTransferModel>)Session["cart"];

                }

                ViewBag.cartItems = li;
                return View();
            }
            catch(Exception ex)
            {
               return View( "not found");
            }
          
        }

        [HttpPost]
        public async Task<ActionResult> Add(string itemId)
        {
           
            int _cartCount = await AddItemToCart(long.Parse(itemId), 1,null);

            JsonResult result = this.Json(new
            {
                cartCount = _cartCount,
            }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        //public async Task<ActionResult> AddWithQuantity(string itemId, string quantity)
        public async Task<ActionResult> AddWithQuantity(CartItem cItem)
        {

           int _cartCount = await AddItemToCart(cItem.itemId, cItem.quantity,cItem.propsValues);
            JsonResult result = this.Json(new
            {
                cartCount = _cartCount,
            }, JsonRequestBehavior.AllowGet);

            return result;
        }
        private async Task<int> AddItemToCart(long itemId, int quantity,List<itemsTransProp> itemProps)
        {
            CategoryController cc = new CategoryController();

            ItemModel item = new ItemModel();
            item = await item.GetItemByID(itemId);

            List<ItemTransferModel> li;
            List<ItemTransferModel> cartItems = new List<ItemTransferModel>() ;
            ItemTransferModel itemFound = null;

            if (Session["cart"] == null)
            {
                li = new List<ItemTransferModel>();

                Session["count"] = 1;

            }
            else
            {
                li = (List<ItemTransferModel>)Session["cart"];

                cartItems = li.Where(x => x.itemUnitId == item.ItemUnitList.FirstOrDefault().itemUnitId).ToList();
                if (!cartItems.Count.Equals(0))
                {
                    itemFound = checkItemProp(cartItems,itemProps);
                }
                if (itemFound == null)
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


                #region default item properties
                List<itemsTransProp> itemsTransProp = new List<itemsTransProp>();

                if (itemProps != null)
                {
                    foreach (var p in itemProps)
                    {
                        itemsTransProp.Add(new itemsTransProp()
                        {
                            itemPropId = p.itemPropId,
                            name = p.name,

                        });
                    }
                }
                #endregion

                li.Add(new ItemTransferModel()
                {
                    itemId = item.itemId,
                    itemName = item.name,
                    itemUnitId = item.ItemUnitList.FirstOrDefault().itemUnitId,
                    price = price,
                    itemUnitPrice = basicPrice,
                    quantity = quantity,
                    offerId = offerId,
                    offerValue = discountValue,
                    offerType = decimal.Parse(discountType),
                    image = item.image,
                    itemsTransProp = itemsTransProp,
                });
            }
            else
                itemFound.quantity += quantity;


            Session["cart"] = li;

            return li.Count();
        }

        private ItemTransferModel checkItemProp(List<ItemTransferModel> cartItems, List<itemsTransProp> list2)
        {
            foreach (var t in cartItems)
            {
                if (t.itemsTransProp.Count.Equals(0) && list2 is null)
                    return t;

                if (t.itemsTransProp != null && !t.itemsTransProp.Count.Equals(0))
                {
                    var eq = (from x in t.itemsTransProp
                              join y in list2 on x.itemPropId equals y.itemPropId
                              select x).ToList();

                    if (eq.Count() == list2.Count())
                        return t;
                }
            }
            return null;
        }


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
                cartCount = li.Count(),
                cartCountStr = li.Count()+ " "+ Global.resourcemanager.GetString("items").ToString(),
                imagePath = Url.Content(Global.APIUriserver + "images/item/"),
            }, JsonRequestBehavior.AllowGet);

            return result;
        }


    }
}
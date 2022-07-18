using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;

namespace PosEcommerce.Controllers
{
    public class ItemController : Controller
    {

        public async Task<ActionResult> Index(int itemId)
        {
            try
            { 
                  ItemModel item = new ItemModel();
                CategoryController cc = new CategoryController();
                item= await item.GetItemByID(itemId);
                item.price = item.ItemUnitList.FirstOrDefault().price;
                if (item.ItemUnitList.FirstOrDefault().offerId != null && item.ItemUnitList.FirstOrDefault().offerId != 0)
                {
                    item.disPrice = cc.GetdiscountPrice(item.ItemUnitList.FirstOrDefault().discountType, item.ItemUnitList.FirstOrDefault().discountValue, item.ItemUnitList.FirstOrDefault().price);

                }
                ViewBag.item = item;


                return View();
            }
            catch(Exception ex)
            {
               return View( "not found");
            }
          
        }
        




    }
}
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
                CategoryModel categorymodel = new CategoryModel();
                CategoryController cc = new CategoryController();
                List<CategoryModel> catpath = new List<CategoryModel>();
                List<CategoryModel> all= new List<CategoryModel>();
                CategoryModel lastpath = new CategoryModel();
                item = await item.GetItemByID(itemId);
                all = await categorymodel.GetAllCategories();
                //offer 
                item.price = item.ItemUnitList.FirstOrDefault().price;
                if (item.ItemUnitList.FirstOrDefault().offerId != null && item.ItemUnitList.FirstOrDefault().offerId != 0)
                {
                    item.disPrice = cc.GetdiscountPrice(item.ItemUnitList.FirstOrDefault().discountType, item.ItemUnitList.FirstOrDefault().discountValue, item.ItemUnitList.FirstOrDefault().price);

                }
                //
                //path
                catpath= cc.GetCategoryPath((int)item.categoryId,all);
                catpath.Last().notes = "";
                lastpath.name = item.name;
                lastpath.categoryId = (int)item.categoryId;
                lastpath.notes = "last";
                catpath.Add(lastpath);
                ViewBag.catPathList = catpath;
                //
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
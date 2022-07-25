using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;
namespace PosEcommerce.Controllers
{
    public class ItemController : Controller
    {

        public async Task<ActionResult> Index(int itemId)
        {
            try
            {
             
                
                #region sesion
                SettingController sc = new SettingController();
                List<SettingModel> settingList = new List<SettingModel>();
                settingList = await sc.setSetting();
                if (Session.Count == 0 || Session["currency"].ToString() == null)
                {
                    Session["settingList"] = settingList;
                    Session["currency"] = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
                    Session["com_name"] = settingList.Where(x => x.settingName == "com_name").FirstOrDefault().value;
                    Session["com_email"] = settingList.Where(x => x.settingName == "com_email").FirstOrDefault().value;
                    Session["com_mobile"] = settingList.Where(x => x.settingName == "com_mobile").FirstOrDefault().value;
                    Session["com_logo"] = settingList.Where(x => x.settingName == "com_logo").FirstOrDefault().value;
                    Session["lang"] = "en";

                    Global.resourcemanager = new ResourceManager("PosEcommerce.AppResource.ar", Assembly.GetExecutingAssembly());
                }
                else
                {
                    //  Session["lang"] = "en";
                }
                sc.checkLang(Session["lang"].ToString());
                ViewBag.path = sc.GetBaseUrl( HttpContext.Request);
                ViewBag.currentp = "products";
                #endregion
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


                return View(item);
            }
            catch(Exception ex)
            {
               return View( "not found");
            }
          
        }



        
    }
}
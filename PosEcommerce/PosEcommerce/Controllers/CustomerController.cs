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
    public class CustomerController : Controller
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
                    Session["accuracy"] = settingList.Where(x => x.settingName == "accuracy").FirstOrDefault().value;
                    Global.accuracy = Session["accuracy"].ToString();
                    Global.currency = Session["currency"].ToString();
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
               

                return View();
            }
            catch(Exception ex)
            {
               return View( "not found");
            }
          
        }



        
    }
}
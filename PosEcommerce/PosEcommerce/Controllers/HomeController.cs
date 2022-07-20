using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace PosEcommerce.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //SettingModel settingmodel = new SettingModel();
            //List<SettingModel> settingList = new List<SettingModel>();
            //settingList = await settingmodel.GetSetting();
            //Global.currency = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
            //ViewBag.currency = Global.currency;
          
          
                SettingController sc = new SettingController();
            List<SettingModel> settingList = new List<SettingModel>();
            settingList= await sc.setSetting();
            if (Session.Count ==0 || Session["currency"].ToString() == null)
            {
                Session["settingList"] = settingList;
                Session["currency"] = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
                Session["com_name"] = settingList.Where(x => x.settingName == "com_name").FirstOrDefault().value;
                Session["com_email"] = settingList.Where(x => x.settingName == "com_email").FirstOrDefault().value;
                Session["com_mobile"] = settingList.Where(x => x.settingName == "com_mobile").FirstOrDefault().value;
                Session["com_logo"] = settingList.Where(x => x.settingName == "com_logo").FirstOrDefault().value;
                Session["lang"] = "en";

                //if (Session["lang"] == "en")
                //{
                //  //    Resources res = new Resources();
                //    Global.resourcemanager = new ResourceManager(Resources.ResourceEn.ResourceManager.BaseName, Assembly.GetExecutingAssembly());



                //}
                //else
                //{

                //    Global.resourcemanager = new ResourceManager(Resources.ResourceAr.ResourceManager.BaseName, Assembly.GetExecutingAssembly());

                //}

                //  Global.resourcemanager.GetString("AboutUs");
            }
            else
            {
              //  Session["lang"] = "en";
            }
            sc.checkLang(Session["lang"].ToString());
            // Resources.Resource1
            // PosEcommerce.Resources.Resource1
            // Resources.ResourceEn
            // ViewBag.about = @Global.resourcemanager.GetString("AboutUs").ToString();
            return View();
        }
      
    
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
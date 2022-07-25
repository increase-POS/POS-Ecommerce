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
            ViewBag.currentp = "home";
            #region session

            SettingController sc = new SettingController();
            List<SettingModel> settingList = new List<SettingModel>();
            settingList = await sc.setSetting();
            ViewBag.path = sc.GetBaseUrl(HttpContext.Request);
            if (Session.Count == 0 || Session["currency"].ToString() == null)
            {
                Session["settingList"] = settingList;
                Session["currency"] = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
                Session["com_name"] = settingList.Where(x => x.settingName == "com_name").FirstOrDefault().value;
                Session["com_email"] = settingList.Where(x => x.settingName == "com_email").FirstOrDefault().value;
                Session["com_mobile"] = settingList.Where(x => x.settingName == "com_mobile").FirstOrDefault().value;
                Session["com_logo"] = settingList.Where(x => x.settingName == "com_logo").FirstOrDefault().value;
                Session["lang"] = "en";

                
            }
            else
            {
                //  Session["lang"] = "en";
            }
            sc.checkLang(Session["lang"].ToString());
            #endregion
 
            return View();
        }
      
    
        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.currentp = "about";
            #region session

            SettingController sc = new SettingController();
            List<SettingModel> settingList = new List<SettingModel>();
            settingList = await sc.setSetting();
            ViewBag.path = sc.GetBaseUrl(HttpContext.Request);
            if (Session.Count == 0 || Session["currency"].ToString() == null)
            {
                Session["settingList"] = settingList;
                Session["currency"] = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
                Session["com_name"] = settingList.Where(x => x.settingName == "com_name").FirstOrDefault().value;
                Session["com_email"] = settingList.Where(x => x.settingName == "com_email").FirstOrDefault().value;
                Session["com_mobile"] = settingList.Where(x => x.settingName == "com_mobile").FirstOrDefault().value;
                Session["com_logo"] = settingList.Where(x => x.settingName == "com_logo").FirstOrDefault().value;
                Session["lang"] = "en";


            }
            else
            {
                //  Session["lang"] = "en";
            }
            sc.checkLang(Session["lang"].ToString());
            #endregion
            return View();
        }

        public async Task<ActionResult> Contact()
        {
            #region session

            SettingController sc = new SettingController();
            List<SettingModel> settingList = new List<SettingModel>();
            settingList = await sc.setSetting();
            ViewBag.path = sc.GetBaseUrl(HttpContext.Request);
            if (Session.Count == 0 || Session["currency"].ToString() == null)
            {
                Session["settingList"] = settingList;
                Session["currency"] = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
                Session["com_name"] = settingList.Where(x => x.settingName == "com_name").FirstOrDefault().value;
                Session["com_email"] = settingList.Where(x => x.settingName == "com_email").FirstOrDefault().value;
                Session["com_mobile"] = settingList.Where(x => x.settingName == "com_mobile").FirstOrDefault().value;
                Session["com_logo"] = settingList.Where(x => x.settingName == "com_logo").FirstOrDefault().value;
                Session["lang"] = "en";


            }
            else
            {
                //  Session["lang"] = "en";
            }
            sc.checkLang(Session["lang"].ToString());
            #endregion
            ViewBag.Message = "Your contact page.";
            ViewBag.currentp = "contact";
           
            return View();
        }
        public string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

    }
}
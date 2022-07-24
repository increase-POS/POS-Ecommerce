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
    public class SettingController : Controller
    {

        public async Task<ActionResult> ChangeLang(string lang,string uri)
        {
            //SettingModel settingmodel = new SettingModel();
            //List<SettingModel> settingList = new List<SettingModel>();
            //settingList = await settingmodel.GetSetting();
            //Global.currency = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
            //ViewBag.currency = Global.currency;


            if (lang=="en") {
                Session["lang"] = "en";
            } else
            {
                Session["lang"] = "ar";
            }
        
            checkLang(lang);
            // Resources.Resource1
            // PosEcommerce.Resources.Resource1
            // Resources.ResourceEn
            // ViewBag.about = @Global.resourcemanager.GetString("AboutUs").ToString();
            //   return RedirectToAction("Index","Home");
            return Redirect(uri);  
        }
        public async Task<List<SettingModel>> setSetting()
        {

            List<SettingModel> settingList = new List<SettingModel>();
            try
            {


                SettingModel settingmodel = new SettingModel();

                settingList = await settingmodel.GetSetting();

                return settingList;
            }


            catch (Exception ex)
            {
                return settingList;
            }

        }
        public void checkLang(string lang)
        {
            try
            {

                if (lang == "en")
                {
                    //    Resources res = new Resources();
                    Global.resourcemanager = new ResourceManager(Resources.ResourceEn.ResourceManager.BaseName, Assembly.GetExecutingAssembly());
                }
                else
                {

                    Global.resourcemanager = new ResourceManager(Resources.ResourceAr.ResourceManager.BaseName, Assembly.GetExecutingAssembly());
                }

            }
            catch (Exception ex)
            {
                Global.resourcemanager = new ResourceManager(Resources.ResourceEn.ResourceManager.BaseName, Assembly.GetExecutingAssembly());

            }

        }
        public string GetBaseUrl(HttpRequestBase request)
        {
          //  var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;
            if (appUrl != "/")
                appUrl = "/" + appUrl;
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            var uri = new Uri(baseUrl);
            string secondPart = uri.LocalPath;
            secondPart = secondPart.TrimStart('/').TrimStart('/');

            if (secondPart != "")
            {
                secondPart = "/" + secondPart;
            }

            return secondPart;
        }
    }
}
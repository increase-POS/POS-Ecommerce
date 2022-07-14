using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;

namespace PosEcommerce.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            SettingModel settingmodel = new SettingModel();
            List<SettingModel> settingList = new List<SettingModel>();
            settingList = await settingmodel.GetSetting();
            Global.currency = settingList.Where(x => x.settingName == "currency").FirstOrDefault().value;
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
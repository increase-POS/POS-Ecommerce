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
    }
}
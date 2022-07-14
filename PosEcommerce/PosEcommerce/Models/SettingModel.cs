using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
namespace PosEcommerce.Models
{
    public class SettingModel
    {

        #region Attributes
        public int valId { get; set; }
        public string value { get; set; }
        public Nullable<int> isDefault { get; set; }
        public Nullable<int> isSystem { get; set; }
        public string notes { get; set; }
        public Nullable<int> settingId { get; set; }
        //setting
        public string settingName { get; set; }
        public string settingNotes { get; set; }


        #endregion

        #region methods
        public async Task<List<SettingModel>> GetSetting()
        {

            List<SettingModel> list = new List<SettingModel>();
         
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetSetting");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<SettingModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        
        }

        #endregion
    }
}

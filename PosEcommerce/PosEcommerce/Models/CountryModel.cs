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
    public class CountryModel
    {

        #region Attributes
        public int countryId { get; set; }
        public string code { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public byte isDefault { get; set; }
        public int currencyId { get; set; }


        #endregion

        #region methods
        public async Task<List<CountryModel>> getCountries()
        {

            List<CountryModel> list = new List<CountryModel>();
         
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/getCountries");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<CountryModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        
        }

        #endregion
    }
}

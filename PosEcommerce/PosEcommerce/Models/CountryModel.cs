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
    public class cityModel
    {
        public int cityId { get; set; }
        public string cityCode { get; set; }
        public Nullable<int> countryId { get; set; }
    }
    public class CountryModel
    {

        #region Attributes
        public int countryId { get; set; }
        public string code { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public byte isDefault { get; set; }
        public int currencyId { get; set; }
        public List<cityModel> citiesList { get; set; }

        #endregion

        #region methods
        public async Task<CountryModel> GetDefaultCountry()
        {

            CountryModel item = new CountryModel();
         
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetDefaultCountry");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<CountryModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                        }
            }
            return item;

        
        }

        #endregion
    }
}

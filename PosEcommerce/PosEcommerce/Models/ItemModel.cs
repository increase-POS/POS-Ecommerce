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
    public class ItemModel
    {
        #region Attributes
        public int? itemId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public Nullable<decimal> taxes { get; set; }
        public Nullable<byte> isActive { get; set; }
        public Nullable<int> min { get; set; }
        public Nullable<int> max { get; set; }
        public Nullable<int> categoryId { get; set; }
        public string categoryName { get; set; }
        public Nullable<int> parentId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }

        public Nullable<int> minUnitId { get; set; }
        public Nullable<int> maxUnitId { get; set; }
        public Nullable<int> itemCount { get; set; }



        public Nullable<int> isNew { get; set; }
        public Nullable<int> isOffer { get; set; }
        // unit item
        public string parentName { get; set; }

        public string minUnitName { get; set; }
        public string maxUnitName { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> basicPrice { get; set; }
        public Nullable<decimal> priceTax { get; set; }
        public List<ItemUnitModel> ItemUnitList { get; set; }
        public Nullable<decimal> disPrice { get; set; }

        public List<Property> Properties { get; set; }
        #endregion


        #region methods
        public async Task<List<ItemModel>> GetAllItems()
        {
            List<ItemModel> list = new List<ItemModel>();
         
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetAllItems");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<ItemModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        }

        public async Task<string> downloadImage(string imageName)
        {
            string tmpPath = "";
            Stream jsonString = null;
            byte[] byteImg = null;
            Image img = null;
            // ... Use HttpClient.
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                client.BaseAddress = new Uri(Global.APIUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                client.DefaultRequestHeaders.Add("Keep-Alive", "3600");
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(Global.APIUri + "Items/GetImage?imageName=" + imageName);
                request.Method = HttpMethod.Get;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStreamAsync();
                    img = Bitmap.FromStream(jsonString);
                    byteImg = await response.Content.ReadAsByteArrayAsync();

                    // configure trmporery path
                    string dir = Directory.GetCurrentDirectory();
                    tmpPath = dir+ Global.CategoryFolder;
                    tmpPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~\\images\\item"));
                    if (!Directory.Exists(tmpPath))
                        Directory.CreateDirectory(tmpPath);
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(imageName);
                    string[] files = System.IO.Directory.GetFiles(tmpPath, fileName + ".*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }
                    tmpPath = Path.Combine(tmpPath, imageName);

                    //if (System.IO.File.Exists(tmpPath))
                    //{
                    //    System.IO.File.Delete(tmpPath);
                    //}
                    using (FileStream fs = new FileStream(tmpPath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        fs.Write(byteImg, 0, byteImg.Length);
                    }
                }
                return tmpPath;
            }
        }

        public async Task<ItemModel> GetItemByID(long itemId)
        {
            ItemModel item = new ItemModel();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("itemId", itemId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetItemById", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<ItemModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }
            return item;
        }
        #endregion
    }

    public class Property
    {
        public int propertyId { get; set; }
        public string name { get; set; }

        public List<ItemProps> ItemPropValues { get; set; }

    }
    public class ItemProps
    {
        public int itemPropId { get; set; }
        public Nullable<int> propertyItemId { get; set; }
        public Nullable<int> itemId { get; set; }
        public Nullable<int> isActive { get; set; }
        public string propValue { get; set; }
        public string propName { get; set; }
        public Nullable<short> isDefault { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
    }
}
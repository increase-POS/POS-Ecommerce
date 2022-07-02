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
    public class CategoryModel
    {
        #region Attributes
        public int categoryId { get; set; }
        public string categoryCode { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public string image { get; set; }
        public Nullable<decimal> taxes { get; set; }
        public Nullable<byte> fixedTax { get; set; }
        public Nullable<int> parentId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<byte> isActive { get; set; }
      
        public Nullable<int> userId { get; set; }
        public Nullable<int> sequence { get; set; }
        public Nullable<int> id { get; set; }
        public string notes { get; set; }
        public Nullable<int> itemsCount { get; set; }
        public List<CategoryModel> childCategories { get; set; }
      
        #endregion


        #region methods
        public async Task<List<CategoryModel>> GetAllCategories()
        {
            List<CategoryModel> list = new List<CategoryModel>();
         
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetAllCategories");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<CategoryModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        }
        //categories of category
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
                request.RequestUri = new Uri(Global.APIUri + "Categories/GetImage?imageName=" + imageName);
                request.Method = HttpMethod.Get;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStreamAsync();
                    img = Bitmap.FromStream(jsonString);
                    byteImg = await response.Content.ReadAsByteArrayAsync();

                    // configure trmporery path
                 //   string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    string dir = Directory.GetCurrentDirectory();
                    //string tmpPath = Path.Combine(dir, Global.TMPFolder);
                    //tmpPath = Path.Combine(dir, Global.CategoryFolder);
                    tmpPath = dir+ Global.CategoryFolder;
                    // tmpPath= Path.Combine(HttpContext.Current.Server.MapPath("~"), Global.CategoryFolder);
                    //  tmpPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), Global.CategoryFolder);
                     tmpPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~\\images\\category"));
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
        //public async Task<string> downloadImage(string imageName)
        //{
        //    Stream jsonString = null;
        //    byte[] byteImg = null;
        //    Image img = null;
        //    string imgDataURL = "";
        //    string tmpPath = "";
        //    // ... Use HttpClient.
        //    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        //    using (var client = new HttpClient())
        //    {
        //        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //        client.BaseAddress = new Uri(Global.APIUri);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
        //        client.DefaultRequestHeaders.Add("Keep-Alive", "3600");
        //        HttpRequestMessage request = new HttpRequestMessage();
        //        request.RequestUri = new Uri(Global.APIUri + "Categories/GetImage?imageName=" + imageName);
        //        request.Method = HttpMethod.Get;
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = await client.SendAsync(request);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            jsonString = await response.Content.ReadAsStreamAsync();
        //            img = Bitmap.FromStream(jsonString);
        //            byteImg = await response.Content.ReadAsByteArrayAsync();

        //            // configure trmporery path
        //            string dir = Directory.GetCurrentDirectory();
        //             tmpPath = Path.Combine(dir, Global.CategoryFolder);
        //         //   var tmpPath = Path.Combine(dir, Global.TMPAgentsFolder);
        //            if (!Directory.Exists(tmpPath))
        //                Directory.CreateDirectory(tmpPath);
        //            tmpPath = Path.Combine(tmpPath, imageName);
        //            if (System.IO.File.Exists(tmpPath))
        //            {
        //                System.IO.File.Delete(tmpPath);
        //            }
        //            using (FileStream fs = new FileStream(tmpPath, FileMode.Create, FileAccess.ReadWrite))
        //            {
        //                fs.Write(byteImg, 0, byteImg.Length);

        //            }

        //            string imreBase64Data = Convert.ToBase64String(byteImg);
        //            imgDataURL = string.Format("data:image/jpeg;base64,{0}", imreBase64Data);
        //        }
        //        return tmpPath;
        //    }
        //}

        public async Task<List<CategoryModel>> GetCategoryPath(int categoryId)
        {
            List<CategoryModel> items = new List<CategoryModel>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("itemId", categoryId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetCategoryPath", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    items.Add(JsonConvert.DeserializeObject<CategoryModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return items;
        }
        public async Task<List<int>> GetCategoriesOfparent(int categoryId)
        {
            List<int> items = new List<int>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("itemId", categoryId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetCategoriesOfparent", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    items.Add(JsonConvert.DeserializeObject<int>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return items;
        }
   
        #endregion
    }
}
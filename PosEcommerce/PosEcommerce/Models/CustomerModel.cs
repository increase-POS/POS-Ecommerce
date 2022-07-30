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
    public class CustomerModel
    {
        #region Attributes
        public int agentId { get; set; }
       
        public string name { get; set; }
        public string code { get; set; }
        public string company { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string accType { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<byte> balanceType { get; set; }
   
        public string notes { get; set; }
        public Nullable<byte> isActive { get; set; }
        public string fax { get; set; }
        public Nullable<decimal> maxDeserve { get; set; }
        public bool isLimited { get; set; }
        public string payType { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public Nullable<int> countryId { get; set; }
        public Nullable<int> cityId { get; set; }
        public string language { get; set; }
        public Nullable<bool> isShopCustomer { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public string countryName { get; set; }
        public string cityName { get; set; }
        #endregion


        #region methods

        public async Task<CustomerModel> GetloginCustomer(string userName, string password)
        {
            CustomerModel user = new CustomerModel();

            //########### to pass parameters (optional)
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("userName", userName);
            parameters.Add("password", password);
            IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetloginCustomer", parameters);
            //#################

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    user = JsonConvert.DeserializeObject<CustomerModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }
            return user;
        }

        public async Task<int> SaveCustomer(CustomerModel customer)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = "PosEcommerce/SaveCustomer";

            var myContent = JsonConvert.SerializeObject(customer);
            parameters.Add("itemObject", myContent);
            return await APIResult.post(method, parameters);
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
                request.RequestUri = new Uri(Global.APIUri + "Agent/GetImage?imageName=" + imageName);
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
                    tmpPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~\\images\\agent"));
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

      
        #endregion
    }

   
   
}
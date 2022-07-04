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
    public class ItemUnitModel
    {
        #region parameters
        //offer
        public string offerName { get; set; }
        public Nullable<int> offerId { get; set; }
        public string discountType { get; set; }
        public Nullable<decimal> discountValue { get; set; }
        public Nullable<decimal> disPrice { get; set; }

        //
        public int itemUnitId { get; set; }
        public Nullable<int> itemId { get; set; }
        public Nullable<int> unitId { get; set; }
        public Nullable<int> unitValue { get; set; }
        public Nullable<short> defaultSale { get; set; }

        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> basicPrice { get; set; }
        public Nullable<decimal> priceTax { get; set; }
        public Nullable<decimal> cost { get; set; }
        public string barcode { get; set; }
        public string mainUnit { get; set; }
        public string smallUnit { get; set; }
        public Nullable<int> subUnitId { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string unitName { get; set; }
        public Nullable<int> storageCostId { get; set; }

        public Nullable<byte> isActive { get; set; }
        public Nullable<decimal> taxes { get; set; }


        #endregion


        #region methods
        //public async Task<List<ItemModel>> GetAllItems()
        //{
        //    List<ItemModel> list = new List<ItemModel>();
         
        //    IEnumerable<Claim> claims = await APIResult.getList("PosEcommerce/GetAllItems");

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<ItemModel>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

   
        #endregion
    }
}
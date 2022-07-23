using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosEcommerce.Models
{
    public class ItemTransferModel
    {
        public int itemsTransId { get; set; }
        public Nullable<int> itemId { get; set; }
        public string itemName { get; set; }
        public Nullable<long> quantity { get; set; }
        public Nullable<long> lockedQuantity { get; set; }
        public Nullable<long> newLocked { get; set; }
        public Nullable<long> availableQuantity { get; set; }
        public Nullable<int> invoiceId { get; set; }
        public Nullable<int> inventoryItemLocId { get; set; }
        public string invNumber { get; set; }
        public Nullable<int> locationIdNew { get; set; }
        public Nullable<int> locationIdOld { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public string notes { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> itemUnitId { get; set; }
        public Nullable<int> offerId { get; set; }
        public Nullable<decimal> offerValue { get; set; }
        public Nullable<decimal> offerType { get; set; }
        public Nullable<decimal> itemTax { get; set; }
        public Nullable<decimal> itemUnitPrice { get; set; }
        public string itemSerial { get; set; }
        public string unitName { get; set; }
        public string barcode { get; set; }
        public string itemType { get; set; }
        public string cause { get; set; }
        public Nullable<decimal> subTotal { get; set; }
        public string image { get; set; }

        public List<itemsTransProp> itemsTransProp { get; set; }
    }
    public  class itemsTransProp
    {
        public int itemsTransPropId { get; set; }
        public Nullable<int> itemPropId { get; set; }
        public Nullable<int> itemsTransId { get; set; }
        public string notes { get; set; }
        public string name { get; set; }
        public Nullable<int> propertyId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosEcommerce.Models
{
    [Serializable]
    public class CartItem
    {
       public long itemId { get; set; }
       public int quantity { get; set; }

        public List<itemsTransProp> propsValues { get; set; }

    }
}
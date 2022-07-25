using PosEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PosEcommerce.Helper
{
    public static class CustomHelper
    {
        public static string multiplyValues(this HtmlHelper helper, decimal value1, int value2)
        {
           
            return (value1 * value2).ToString();
        }
        public static string itemWithPropDesc(this HtmlHelper helper, ItemTransferModel item)
        {
            string str = item.itemName;
            if (item.itemsTransProp.Count > 0)
                str += ": ";

            int i = 0;
            foreach (var p in item.itemsTransProp)
            {
                if (i.Equals(0))
                    str += p.name;
                else
                    str += " ," + p.name;
            }

            return str;
        }

    }
}
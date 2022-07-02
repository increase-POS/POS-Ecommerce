using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;

namespace PosEcommerce.Controllers
{
    public class ItemController : Controller
    {

        public async Task<ActionResult> Index(int itemId)
        {
            try
            {
                ItemModel item = new ItemModel();
               ViewBag.item=await item.GetItemByID(itemId);
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
          
        }
        




    }
}
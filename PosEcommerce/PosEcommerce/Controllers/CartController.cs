using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;

namespace PosEcommerce.Controllers
{
    public class CartController : Controller
    {

        public async Task<ActionResult> CartView()
        {
            try
            { 
            


                return View();
            }
            catch(Exception ex)
            {
               return View( "not found");
            }
          
        }
        




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;
namespace PosEcommerce.Controllers
{
    public class CustomerController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> saveCustomer(CustomerModel customer)
        {
            try
            {
                int id = 0;
                string res = "";
                if (Session["customer"]==null)
                {
                    //add
                    customer.language = Session["lang"].ToString();
                    customer.type = "c";
                    customer.isActive = 1;
                    id = await customer.SaveCustomer(customer);
                }
                else
                {
                    //EDIT
                    CustomerModel oldcustomer = Session["customer"] as CustomerModel;
                     
                    if (oldcustomer.agentId > 0)
                    {
                        customer.agentId = oldcustomer.agentId;
                        customer.language= Session["lang"].ToString();
                        
                        // customer.type = "c";
                        // customer.isActive = 1;
                        customer.notes = oldcustomer.notes;
                        id = await customer.SaveCustomer(customer);
                    }

                   

                }
                
              
               
                if (id > 0)
                {
                    res = "saved";
                    customer.agentId = id;
                    Session["customer"] = customer;
                }
                else
                {
                    res = "notsaved";
                }
             

                JsonResult result = this.Json(new
                {
                   msg = res,
                }, JsonRequestBehavior.AllowGet);

                return result;
                #region sesion
             
                #endregion


               
            }
            catch(Exception ex)
            {
               return View( "not found");
            }
          
        }


        [HttpPost]
        public async Task<ActionResult> loginCustomer(CustomerModel customer)
        {
            try
            {
               // customer.language = Session["lang"].ToString();
                string res = "";
                customer = await customer.GetloginCustomer(customer.userName,customer.password);
                if (customer.agentId > 0)
                {

                    res = "correct";
                    //  Session["agentId"] = id;
                    Session["customer"] = customer;
                    Session["lang"] = customer.language;
                }
                else
                {
                    res = "wrong username or password";
                }
               

                JsonResult result = this.Json(new
                {
                    msg = res,
                }, JsonRequestBehavior.AllowGet);

                return result;
                #region sesion
                
                #endregion


                // return View();
            }
            catch (Exception ex)
            {
                return View("not found");
            }

        }


    }
}
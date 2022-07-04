using System;
using System.Web;

namespace PosEcommerce.Models
{
    public class PaginateModel  
    {

        #region Attributes
        public int rowsInPage { get; set; }
        public int? currentPage { get; set; }
        public int allpagesCount { get; set; }
     
  


     

        #endregion
    }
}

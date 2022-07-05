using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace PosEcommerce.Models
{
    public class Global
    {
        public static string APIUri = "http://localhost:44730/api/";
        public static string APIUriserver = "http://localhost:44730/";
        //public static string APIUri = "http://192.168.1.5:44370/api/";

        #region pagination settings
        public static int rowsInPage = 12;
        #endregion
        #region folders pathes
        public const string TMPFolder = "C:/Temp/Thumb";
        public const string TMPUsersFolder = "C:/Temp/Thumb/users";
        public const string TMPAgentsFolder = "C:/Temp/Thumb/agents";
        public const string CategoryFolder = "/images/category";
        public static  string Categorypath = Path.Combine("~\\images\\category\\");
        //public const string CategoryFolder = "C:/Temp/Thumb/images/category";
        #endregion

        #region global parameters
        public static string accuracy;
        public static string currency;
        #endregion

    }
}
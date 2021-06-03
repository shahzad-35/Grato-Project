using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace PointOfSaleW
{
  static  class Global
    {
     public static List<BESaleDetailList> items = null;
     public static List<BERecipeDetailList> Recipeitems = null;
     public static List<BESaleDetailEdit> SaleDetailList;

     public static string Branch { get; set; }
     public static int TokenId { get; set; }
     public static int BranchId { get; set; }
     public static int SaleIdEdit { get; set; }
     public static int TokenIdEdit { get; set; }
     public static int RecipeId { get; set; }
     public static string Password { get; set; }
     public static int SaleId { get; set; }
     public static string DateFrom {get;set;}
     public static string DateTo {get;set;}
     public static byte[] FileBuffer { get; set; }
     public static decimal CashRevieved { get; set; }
     public static decimal CashReturn { get; set; }

     public static List<int> JalebiOrderList = new List<int>();
     public static List<int> SamosaOrderList = new List<int>();
     public static List<KeyValuePair<int,int>> OtherOrderList = new List<KeyValuePair<int,int>>();

     public static String CustomerDataSetName = "CustomerDataSet";
     public static String CustomerRecietRdlcPath
         = @"C:\Users\mpc\Documents\Visual Studio 2013\Projects\PointOfSaleW\PointOfSaleW\CustomerRecietRdlc.rdlc";

     public static String CheckReportDataSetName = "CheckReport";
     public static String CheckReportPath
         = @"C:\Users\mpc\Documents\Visual Studio 2013\Projects\PointOfSaleW\PointOfSaleW\ReportCheckRDLC.rdlc"; 

    }
}

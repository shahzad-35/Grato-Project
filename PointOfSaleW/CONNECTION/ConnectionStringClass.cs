using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CONNECTION
{
   public  class ConnectionStringClass
    {
        public static string path;
        static ConnectionStringClass()
        {
            path = ConfigurationManager.ConnectionStrings["PointOfSale"].ConnectionString;

        }
    }
}

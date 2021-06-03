using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public  class BERecipeSaleList
    {
      public int ProductId { get; set; }
      public string ProductName { get; set; }
      public decimal UnitPrice { get; set; }
      public decimal TotalUnits { get; set; }
      public decimal TotalPrice { get; set; }
     
    }
}

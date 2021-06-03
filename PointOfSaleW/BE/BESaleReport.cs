using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BESaleReport
    {
        public int SaleId { get; set; }
        public int TokenId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalUnits { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalSale { get; set; }
        public decimal Discount { get; set; }
        public decimal NetSale { get; set; }
        public decimal CashRecieved { get; set; }
        public decimal CashReturn { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }

    }
}

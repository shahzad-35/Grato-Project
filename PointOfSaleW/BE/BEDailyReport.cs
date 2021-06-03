using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public   class BEDailyReport
    {
        public int SaleId { get; set; }
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalUnits { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
    }
}

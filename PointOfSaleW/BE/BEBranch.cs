using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public  class BEBranch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; } 
    }
}

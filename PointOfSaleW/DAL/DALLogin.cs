using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;
using CONNECTION;

namespace DAL
{
   public class DALLogin
    {
        private string path = ConnectionStringClass.path;


        public List<BEBranch> BranchSelect()
        {
            //DESKTOP-N54A1QE\SQLEXPRESS
            //GRATO-PC\SQLEXPRESS

            List<BEBranch> list = new List<BEBranch>();
            SqlConnection con = new SqlConnection(path);

            SqlCommand cmd = new SqlCommand(StoreProc.Name.spBranchGet.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BEBranch be = new BEBranch();
                    be.BranchId = Convert.ToInt32(read["BranchId"]);
                    be.BranchName = Convert.ToString(read["BranchName"]);
                    be.UserName = Convert.ToString(read["UserName"]);
                    be.Password = Convert.ToString(read["Password"]);
                    list.Add(be);
                }

            }

            con.Close();
            return list;
        }
  
    }
}

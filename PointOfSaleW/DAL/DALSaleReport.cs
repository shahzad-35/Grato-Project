using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;
using CONNECTION;
using System.Collections.Generic;
using System;


namespace DAL
{
    public class DALSaleReport
    {


        private string path = ConnectionStringClass.path;
        public List<BESaleReport> SaleReportSelectById(int SaleId)
        {
            List<BESaleReport> list = new List<BESaleReport>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spGetRecietDataById.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SaleId", SaleId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BESaleReport be = new BESaleReport();
                    be.SaleId = Convert.ToInt32(read["SaleId"]);
                    be.TokenId = Convert.ToInt32(read["TokenId"]);
                    be.ProductId = Convert.ToInt32(read["ProductId"]);
                    be.ProductName = Convert.ToString(read["ProductName"]);
                    be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                    be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
                    be.TotalPrice = Convert.ToDecimal(read["TotalPrice"]);
                    be.TotalSale = Convert.ToDecimal(read["TotalSale"]);
                    be.Discount = Convert.ToDecimal(read["Discount"]);
                    be.NetSale = Convert.ToDecimal(read["NetSale"]);
                    be.BranchId = Convert.ToInt32(read["BranchId"]);
                    be.CashRecieved = Convert.ToDecimal(read["CashRecieved"]);
                    be.CashReturn = Convert.ToDecimal(read["CashReturn"]);


                    list.Add(be);
                }

            }


            con.Close();
            return list;
        }

        public List<BEDailyReport> DailyReportGetByDate(BEDailyReport bee)
        {
            List<BEDailyReport> list = new List<BEDailyReport>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spGetDataByDate.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchId", bee.BranchId);
            cmd.Parameters.AddWithValue("@StartDate", bee.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", bee.EndDate);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BEDailyReport be = new BEDailyReport();
                    be.ProductName = Convert.ToString(read["ProductName"]);
                    be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
                    be.TotalPrice = Convert.ToDecimal(read["TotalPrice"]);
                    list.Add(be);
                }

            }


            con.Close();
            return list;
        }

        public List<BEDailyReport> DailyReportGetByCurrentDate(BEDailyReport bee)
        {
            List<BEDailyReport> list = new List<BEDailyReport>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spGetDataByCurrentDate.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchId", bee.BranchId);
            cmd.Parameters.AddWithValue("@StartDate", bee.StartDate);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BEDailyReport be = new BEDailyReport();
                    be.ProductName = Convert.ToString(read["ProductName"]);
                    be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
                    be.TotalPrice = Convert.ToDecimal(read["TotalPrice"]);
                    list.Add(be);
                }

            }


            con.Close();
            return list;
        }

    }

}


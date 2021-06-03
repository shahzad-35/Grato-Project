using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;
using CONNECTION;
using System.Collections.Generic;
using System;
using System.ComponentModel;



namespace DAL
{
   public class DALSale
    {
      

        private string path = ConnectionStringClass.path;


        #region Methods
        
        
       public int GetLastTokenId()
        {
            DateTime DateTime = DateTime.Now;
            var date = DateTime.Date; 
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spTokenIdSelect.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DateNow", date); 
            con.Open();
            string tokenID = Convert.ToString(cmd.ExecuteScalar());
            con.Close();

            return tokenID == "" ? 1 : (Convert.ToInt32(tokenID) + 1);
           
        } 
        
       public int GetLastTokenIdForSaleDetail()
        {
            DateTime DateTime = DateTime.Now;
            var date = DateTime.Date;
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spTokenIdSelect.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DateNow", date);
            con.Open();
            string tokenID = Convert.ToString(cmd.ExecuteScalar());
            con.Close();

            return Convert.ToInt32(tokenID);

        }
        
       public int SaleInsert(BESale be)
        {
            
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleInsert.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TokenId", GetLastTokenId()); 
            cmd.Parameters.AddWithValue("@TotalSale", be.TotalSale);
            cmd.Parameters.AddWithValue("@Discount", be.Discount);
            cmd.Parameters.AddWithValue("@NetSale", be.NetSale);
            cmd.Parameters.AddWithValue("@BranchId", be.BranchId);
            cmd.Parameters.AddWithValue("@CreatedDate", be.CreatedDate);
            cmd.Parameters.AddWithValue("@CashRecieved", be.CashRecieved);
            cmd.Parameters.AddWithValue("@CashReturn", be.CashReturn);

            con.Open();
            int Id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Id;
        }

       public int SaleInsertEdit(BESale be)
        {
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleInsertEdit.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SaleId", be.SaleId);
            cmd.Parameters.AddWithValue("@TotalSale", be.TotalSale);
            cmd.Parameters.AddWithValue("@Discount", be.Discount);
            cmd.Parameters.AddWithValue("@NetSale", be.NetSale);
            cmd.Parameters.AddWithValue("@BranchId", be.BranchId);
            cmd.Parameters.AddWithValue("@CreatedDate", be.CreatedDate);
            cmd.Parameters.AddWithValue("@CashRecieved", be.CashRecieved);
            cmd.Parameters.AddWithValue("@CashReturn", be.CashReturn); 
            con.Open();
            int Id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Id;
        }

       /// <summary>
       /// editing sale against TokenId
       /// </summary>
       /// <param name="be">class object</param>
       /// <returns> SaleId</returns>
       public int SaleInsertEditByTokenId(BESale be)
       {
           SqlConnection con = new SqlConnection(path);
           SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleInsertEditByTokenId.ToString(), con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@TokenId", be.TokenId);
           cmd.Parameters.AddWithValue("@TotalSale", be.TotalSale);
           cmd.Parameters.AddWithValue("@Discount", be.Discount);
           cmd.Parameters.AddWithValue("@NetSale", be.NetSale);
           cmd.Parameters.AddWithValue("@BranchId", be.BranchId);
           cmd.Parameters.AddWithValue("@CreatedDate", be.CreatedDate);
           cmd.Parameters.AddWithValue("@CashRecieved", be.CashRecieved);
           cmd.Parameters.AddWithValue("@CashReturn", be.CashReturn);
           con.Open();
           int saleId = Convert.ToInt32(cmd.ExecuteScalar());
           con.Close();
           return saleId;
       }
        
       public int SaleReturn(int SaleId)
        {
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleReturn.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SaleId", SaleId); 
            con.Open();
            int Id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Id;
        }


        
       public void SaveBulkSaleDetail<T>(IList<T> list)
        {

            using (var bulkCopy = new SqlBulkCopy(path))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = "dbo.SaleDetail";

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T))
                                           .Cast<PropertyDescriptor>()
                                           .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                           .ToArray();


                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }


                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }


        }


        
       public List<BESaleDetail> GetSaleDetailDataById(int SaleId)
        {
            List<BESaleDetail> list = new List<BESaleDetail>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleDetailGetById.ToString(), con);
            cmd.Parameters.AddWithValue("@SaleId", SaleId);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BESaleDetail be = new BESaleDetail();
                    be.SaleDetailId = Convert.ToInt32(read["SaleDetailId"]);
                    be.SaleId = Convert.ToInt32(read["SaleId"]);
                    be.ProductId = Convert.ToInt32(read["ProductId"]);
                    be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                    be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
                    be.TotalPrice = Convert.ToDecimal(read["Totalprice"]);
                    be.CreatedDate = Convert.ToDateTime(read["CreatedDate"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }


        
       public List<BESale> GetRecentSaleById()
        {
            List<BESale> list = new List<BESale>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spRecentSaleGetById.ToString(), con);
            cmd.Parameters.AddWithValue("@Datetime", DateTime.Now.Date);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BESale be = new BESale();
                    be.SaleId = Convert.ToInt32(read["SaleId"]);
                    be.TokenId = Convert.ToInt32(read["TokenId"]);
                    be.TotalSale = Convert.ToInt32(read["TotalSale"]);
                    be.Discount = Convert.ToDecimal(read["Discount"]);
                    be.NetSale = Convert.ToDecimal(read["NetSale"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }
        
       public List<BESale> GetSaleByDate()
        {
            List<BESale> list = new List<BESale>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spRecentSaleGetById.ToString(), con);
            cmd.Parameters.AddWithValue("@Datetime", DateTime.Now.Date);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BESale be = new BESale();
                    be.SaleId = Convert.ToInt32(read["SaleId"]);
                    be.TokenId = Convert.ToInt32(read["TokenId"]);
                    be.TotalSale = Convert.ToInt32(read["TotalSale"]);
                    be.Discount = Convert.ToDecimal(read["Discount"]);
                    be.NetSale = Convert.ToDecimal(read["NetSale"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }
        
       public List<BESaleDetailEdit> GetSaleDetailDataByIdEdit(int SaleId)
        {
            List<BESaleDetailEdit> list = new List<BESaleDetailEdit>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleDetailGetByIdEdit.ToString(), con);
            cmd.Parameters.AddWithValue("@SaleId", SaleId);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BESaleDetailEdit be = new BESaleDetailEdit();
                    be.SaleDetailId = Convert.ToInt32(read["SaleDetailId"]);
                    be.ProductName = Convert.ToString(read["ProductName"]);
                    be.SaleId = Convert.ToInt32(read["SaleId"]);
                    be.ProductId = Convert.ToInt32(read["ProductId"]);
                    be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                    be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
                    be.TotalPrice = Convert.ToDecimal(read["Totalprice"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }


       /// <summary>
       /// get sale detail by token id of same date
       /// </summary>
       /// <param name="tokenId"></param>
       /// <returns>list of Sale detail </returns>
       public List<BESaleDetailEdit> GetSaleDetailDataByTokenIdEdit(int tokenId)
       {
           List<BESaleDetailEdit> list = new List<BESaleDetailEdit>();
           SqlConnection con = new SqlConnection(path);
           SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleDetailGetByTokenIdEdit.ToString(), con);
           cmd.Parameters.AddWithValue("@TokenId", tokenId);
           cmd.CommandType = CommandType.StoredProcedure;
           con.Open();
           SqlDataReader read = cmd.ExecuteReader();
           if (read.HasRows)
           {
               while (read.Read())
               {
                   BESaleDetailEdit be = new BESaleDetailEdit();
                   be.SaleDetailId = Convert.ToInt32(read["SaleDetailId"]);
                   be.ProductName = Convert.ToString(read["ProductName"]);
                   be.SaleId = Convert.ToInt32(read["SaleId"]);
                   be.ProductId = Convert.ToInt32(read["ProductId"]);
                   be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                   be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
                   be.TotalPrice = Convert.ToDecimal(read["Totalprice"]);
                   list.Add(be);
               }
               con.Close();
           }
           return list;

       }

       public List<BESaleDetailEdit> DeleteSaleDetailData(int SaleDetailId)
        {
            List<BESaleDetailEdit> list = new List<BESaleDetailEdit>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleDetailDelByIdEdit.ToString(), con);
            cmd.Parameters.AddWithValue("@SaleDetailId", SaleDetailId);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BESaleDetailEdit be = new BESaleDetailEdit();
                    be.SaleDetailId = Convert.ToInt32(read["SaleDetailId"]);
                    be.SaleId = Convert.ToInt32(read["SaleId"]);
                    be.ProductId = Convert.ToInt32(read["ProductId"]);
                    be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                    be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
                    be.TotalPrice = Convert.ToDecimal(read["Totalprice"]);
                    be.CreatedDate = Convert.ToDateTime(read["CreatedDate"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }

        
       int SaleID;
        
       public int SaleCheck(int SaleId)
        {
            
            List<BESale> list = new List<BESale>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleCheckEdit.ToString(), con);
            cmd.Parameters.AddWithValue("@SaleId", SaleId);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    SaleID = Convert.ToInt32(read["SaleId"]); 
                     
                }
                con.Close();
            }
            return SaleID;

        }
        #endregion //methods end

       #region MethodsOrderQueue
       /// <summary>
       /// checking if the tokenid exist of same date
       /// in Sale Table
       /// </summary>
       /// <param name="tokenId"></param>
       /// <returns></returns>
       public int checkTokenId(int tokenId)
       {
           SqlConnection con = new SqlConnection(path);
           SqlCommand cmd = new SqlCommand(StoreProc.Name.spCheckTokenId.ToString(), con);
           cmd.Parameters.AddWithValue("@TokenId", tokenId);
           cmd.CommandType = CommandType.StoredProcedure;
           con.Open();
           SqlDataReader read = cmd.ExecuteReader();
           int id = 0;
           if (read.HasRows)
           {
               while (read.Read())
               {
                   id = Convert.ToInt32(read["TokenId"]);
               }
               con.Close();
               return id;
           }
           else
           {
               return 0;
           }
       }

       /// <summary>
       /// getting tokenId
       /// in OrderQueue Table
       /// </summary>
       /// <param name="tokenId"></param>
       /// <returns></returns>
       public int checkTokenIdOrderQueueTable(int tokenId)
       {
           SqlConnection con = new SqlConnection(path);
           SqlCommand cmd = new SqlCommand(StoreProc.Name.spCheckTokenIdOrderQueue.ToString(), con);
           cmd.Parameters.AddWithValue("@TokenId", tokenId);
           cmd.CommandType = CommandType.StoredProcedure;
           con.Open();
           SqlDataReader read = cmd.ExecuteReader();
           int id = 0;
           if (read.HasRows)
           {
               while (read.Read())
               {
                   id = Convert.ToInt32(read["TokenId"]);
               }
               con.Close();
               return id;
           }
           else
           {
               return 0;
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public List<BEOrderQueue> OrderQueueGet()
       {
           List<BEOrderQueue> list = new List<BEOrderQueue>();
           SqlConnection con = new SqlConnection(path);
           SqlCommand cmd = new SqlCommand(StoreProc.Name.spOrderQueueGet.ToString(), con);
           cmd.CommandType = CommandType.StoredProcedure;
           con.Open();
           SqlDataReader read = cmd.ExecuteReader();
           if (read.HasRows)
           {
               while (read.Read())
               {
                   BEOrderQueue obj = new BEOrderQueue();
                   obj.TokenId = Convert.ToInt32(read["TokenId"]);
                   obj.TypeId = Convert.ToInt32(read["TypeId"]);

                   
                   list.Add(obj);
               }
               con.Close();
           }
           return list;
       }

       /// <summary>
       /// Keep track of order queue, 
       /// so that if something happen to the application,
       /// we will again retrieve list from here
       /// </summary>
       /// <param name="tokenId"></param>
       /// <param name="typeId"></param>
       public void OrderQueueInsert(int tokenId,int typeId)
       {
           SqlConnection con = new SqlConnection(path);
           SqlCommand cmd = new SqlCommand(StoreProc.Name.spOrderQueueInsert.ToString(), con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@TokenId",tokenId);
           cmd.Parameters.AddWithValue("@TypeId", typeId);
           con.Open();
           cmd.ExecuteScalar();
           con.Close();
       }

       /// <summary>
       /// this method will delete order queue list
       /// from database
       /// when order deliver to customer
       /// </summary>
       /// <param name="tokenId"></param>
       /// <param name="typeId">16, 17 OR 18</param>
       public void OrderQueueDelete(int tokenId, int typeId)
       {
           SqlConnection con = new SqlConnection(path);
           SqlCommand cmd = new SqlCommand(StoreProc.Name.spOrderQueueDelete.ToString(), con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@TokenId", tokenId);
           cmd.Parameters.AddWithValue("@TypeId", typeId);
           con.Open();
           cmd.ExecuteScalar();
           con.Close();
       }
       
       #endregion //methods end





    }
}

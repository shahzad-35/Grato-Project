using CONNECTION;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace DAL
{
   public class DALRecipe
    {

        private string path = ConnectionStringClass.path;


        #region Methods


        public int RecipeInsert(BERecipeDef be)
        {
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spRecipeInsert.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecipeName", be.RecipeName); 

            con.Open();
            int Id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Id;
        }


        public void SaveBulkRecipeDetail<T>(IList<T> list)
        {

            using (var bulkCopy = new SqlBulkCopy(path))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = "dbo.RecipeDetail";

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

        public List<BERecipeDef> GetRecipe()
        {
            List<BERecipeDef> list = new List<BERecipeDef>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spRecipeGet.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BERecipeDef be = new BERecipeDef();
                    be.RecipeId = Convert.ToInt32(read["RecipeId"]);
                    be.RecipeName = Convert.ToString(read["RecipeName"]); 
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }

        public List<BERecipeSaleList> GetRecipeById(int RecipeId)
        {
            List<BERecipeSaleList> list = new List<BERecipeSaleList>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spRecipeGetSaleList.ToString(), con);
            cmd.Parameters.AddWithValue("@RecipeId", RecipeId);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BERecipeSaleList be = new BERecipeSaleList();
                    be.ProductId = Convert.ToInt32(read["ProductId"]);
                    be.ProductName = Convert.ToString(read["ProductName"]);
                    be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }

        public List<BERecipeDetail> GetRecipeDetailDataById(int RecipeId)
        {
            List<BERecipeDetail> list = new List<BERecipeDetail>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleDetailGetById.ToString(), con);
            cmd.Parameters.AddWithValue("@RecipeId", RecipeId);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BERecipeDetail be = new BERecipeDetail();
                    be.RecipeDetailId = Convert.ToInt32(read["RecipeDetailId"]);
                    be.RecipeId = Convert.ToInt32(read["RecipeId"]); 
                    be.ProductId = Convert.ToInt32(read["ProductId"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }


        //public List<BESaleDetailEdit> GetSaleDetailDataByIdEdit(int SaleId)
        //{
        //    List<BESaleDetailEdit> list = new List<BESaleDetailEdit>();
        //    SqlConnection con = new SqlConnection(path);
        //    SqlCommand cmd = new SqlCommand(StoreProc.Name.spSaleDetailGetByIdEdit.ToString(), con);
        //    cmd.Parameters.AddWithValue("@SaleId", SaleId);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader read = cmd.ExecuteReader();
        //    if (read.HasRows)
        //    {
        //        while (read.Read())
        //        {
        //            BESaleDetailEdit be = new BESaleDetailEdit();
        //            be.SaleDetailId = Convert.ToInt32(read["SaleDetailId"]);
        //            be.ProductName = Convert.ToString(read["ProductName"]);
        //            be.SaleId = Convert.ToInt32(read["SaleId"]);
        //            be.ProductId = Convert.ToInt32(read["ProductId"]);
        //            be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
        //            be.TotalUnits = Convert.ToDecimal(read["TotalUnits"]);
        //            be.TotalPrice = Convert.ToDecimal(read["Totalprice"]);
        //            list.Add(be);
        //        }
        //        con.Close();
        //    }
        //    return list;

        //}


        public List<BERecipeDetail> DeleteRecipeDetailData(int RecipeId)
        {
            List<BERecipeDetail> list = new List<BERecipeDetail>();
            SqlConnection con = new SqlConnection(path);
            SqlCommand cmd = new SqlCommand(StoreProc.Name.spRecipeDelById.ToString(), con);
            cmd.Parameters.AddWithValue("@RecipeId", RecipeId);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    BERecipeDetail be = new BERecipeDetail();
                    be.RecipeDetailId = Convert.ToInt32(read["RecipeDetailId"]);
                    be.RecipeId = Convert.ToInt32(read["RecipeId"]);
                    be.ProductId = Convert.ToInt32(read["ProductId"]);
                    list.Add(be);
                }
                con.Close();
            }
            return list;

        }


        #endregion //methods end
    }
}

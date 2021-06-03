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
   public class DALProduct
    {
       
           private string path = ConnectionStringClass.path;

       /// <summary>
       /// will get all products from database 
       /// by calling store procedure
       /// </summary>
       /// <returns>list of Products</returns>
           public List<BEProduct> GetAllProducts()
           {

               List<BEProduct> list = new List<BEProduct>();
               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductGet.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               con.Open();
               SqlDataReader read = cmd.ExecuteReader();
               if (read.HasRows)
               {
                   while (read.Read())
                   {
                       BEProduct product = new BEProduct();
                       product.ProductId = Convert.ToInt32(read["ProductId"]);
                       product.ProductName = Convert.ToString(read["ProductName"]);
                       product.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                       product.TypeId = Convert.ToInt32(read["TypeId"]);
                       list.Add(product);
                   }

               }

               con.Close();
               return list;
           }

           public List<BEProduct> GetProductByID(int ProductId)
           {

               List<BEProduct> list = new List<BEProduct>();
               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductGetById.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", ProductId);
               con.Open();
               SqlDataReader read = cmd.ExecuteReader();
               if (read.HasRows)
               {
                   while (read.Read())
                   {
                       BEProduct be = new BEProduct();
                       be.ProductId = Convert.ToInt32(read["ProductId"]);
                       be.ProductName = Convert.ToString(read["ProductName"]);
                       be.UnitPrice = Convert.ToDecimal(read["UnitPrice"]);
                       list.Add(be);
                   }

               }

               con.Close();
               return list;
           }


           public int ProductPriceSelectById(int ProductId)
           {

               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductPriceGetById.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", ProductId);
               con.Open();
               int UnitPrice = Convert.ToInt32(cmd.ExecuteScalar());


               con.Close();
               return UnitPrice;
           }


           public void AddProduct(BEProduct be)
           {
               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductInsert.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductName", be.ProductName);
               cmd.Parameters.AddWithValue("@UnitPrice", be.UnitPrice);
               cmd.Parameters.AddWithValue("@TypeId", be.TypeId);
               con.Open();
               Convert.ToInt32(cmd.ExecuteScalar());
               con.Close();
           }

           public void UpdateProduct(BEProduct be)
           {
               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductUpdate.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", be.ProductId);
               cmd.Parameters.AddWithValue("@ProductName", be.ProductName);
               cmd.Parameters.AddWithValue("@UnitPrice", be.UnitPrice);
               con.Open();
               Convert.ToInt32(cmd.ExecuteScalar());
               con.Close();
           }

           public void DeleteProduct(int ProductId)
           {
               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductDeleteById.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", ProductId);
               con.Open();
               cmd.ExecuteNonQuery();
               con.Close();
           }

           public int ProductDeleteCheck(int ProductId)
           {
               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductDeleteCheckById.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", ProductId);
               con.Open();
               int ProdId = Convert.ToInt32(cmd.ExecuteScalar());
               con.Close();
               return ProdId;
           }

        #region ProductType


           /// <summary>
           /// will get all products types from database 
           /// by calling store procedure
           /// <usage>In Product.xml</usage>
           /// </summary>
           /// <returns>list of Products types</returns>
           public List<BEProductType> GetAllProductsType()
           {

               List<BEProductType> list = new List<BEProductType>();
               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spProductTypeGet.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               con.Open();
               SqlDataReader read = cmd.ExecuteReader();
               if (read.HasRows)
               {
                   while (read.Read())
                   {
                       BEProductType product = new BEProductType();
                       product.TypeId = Convert.ToInt32(read["TypeId"]);
                       product.TypeName = Convert.ToString(read["TypeName"]);
                       list.Add(product);
                   }

               }

               con.Close();
               return list;
           }

           public int GetTypeIdFromProduct(int productId) 
           {
               int typeId = 0;

               SqlConnection con = new SqlConnection(path);
               SqlCommand cmd = new SqlCommand(StoreProc.Name.spGetTypeIdFromProduct.ToString(), con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@ProductId", productId);
               con.Open();
               SqlDataReader read = cmd.ExecuteReader();
               if (read.HasRows)
               {
                   while (read.Read())
                   {
                       typeId = Convert.ToInt32(read["TypeId"]);
                   }

               }

               con.Close();
               return typeId;
           }

        #endregion

    }
}

     
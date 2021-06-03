using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DAL;
using BE; 
using System.Windows; 
using System.Windows.Documents;
namespace PointOfSaleW
{
   public class Common
    {


        //public void CategoryFill(ComboBox DropDownName ddlProduct)
        //{
        //    //BEBranch be = new BEBranch();
        //    //ddlCategory.DataSource = new DALCompany().showcompany();
        //    //ddlCategory.DataTextField = "company";
        //    //ddlCategory.DataValueField = "company_id";
        //    //ddlCategory.DataBind();
        //    //ddlCategory.Items.Insert(0, "....Select....");

        //}

        public void ProductFill(ComboBox  ddlProduct)
        {
            List<BEProduct> LstPro = new List<BEProduct>();
            LstPro = new DALProduct().GetAllProducts();
  
             // ... Get the ComboBox reference. 

	    // ... Assign the ItemsSource to the List.

            ddlProduct.ItemsSource = LstPro;
            ddlProduct.DisplayMemberPath = "BranchName";
            ddlProduct.SelectedValuePath = "BranchId";

            ddlProduct.SelectedValue = "2";
	    ddlProduct.ItemsSource = LstPro;

	    // ... Make the first item selected. 
	

        //}
        //public void ProductHDNFill(DropDownList ddlCategory)
        //{
        //    ddlCategory.DataSource = new DALProduct().ProductSelect();
        //    ddlCategory.DataTextField = "ProductId";
        //    ddlCategory.DataValueField = "UnitPrice";
        //    ddlCategory.DataBind();
        //    ddlCategory.Items.Insert(0, ".....Select....");

        //}
    }
}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;
using DAL;
using System.Text.RegularExpressions;

namespace PointOfSaleW
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public Product()
        {
            InitializeComponent();
  Loaded += MyWindow_Loaded;
        }
        //action on window loaded
        
            List<BEProduct> lst = new List<BEProduct>();
        int ProductId;
            
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {

            btnUpdate.Visibility = System.Windows.Visibility.Hidden;
            lst = new DALProduct().GetAllProducts();
            LVShowProduct.ItemsSource = lst;
             
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
             Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

             if (Patern.IsMatch(txtPrice.Text))
             {
                 lblPriceMsg.Content = "";
                 BEProduct be = new BEProduct();
                 be.ProductName = txtProduct.Text;
                 be.UnitPrice = Convert.ToDecimal(txtPrice.Text);

                 be.TypeId =Convert.ToInt32(cblProductType.SelectedValue);
                 DALProduct obj = new DALProduct();
                 obj.AddProduct(be);

                 txtProduct.Text = null;
                 txtPrice.Text = null;
                 lst = new DALProduct().GetAllProducts();
                 LVShowProduct.ItemsSource = lst;
             }
             else
             {
                 txtPrice.Text = null;
                 lblPriceMsg.Content = "* Invalid";
             }
        } 

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            btnUpdate.Visibility = System.Windows.Visibility.Visible;
            btnAdd.Visibility = System.Windows.Visibility.Hidden;
            Button textBox = (Button)sender; 
             ProductId = ((BE.BEProduct)textBox.DataContext).ProductId ; 
            var GetRow = lst.FirstOrDefault(x => x.ProductId == ProductId); 
             txtProduct.Text=GetRow.ProductName;
             txtPrice.Text =Convert.ToString( GetRow.UnitPrice);

 
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            Button textBox = (Button)sender;
            ProductId = ((BE.BEProduct)textBox.DataContext).ProductId; 
            DALProduct obj = new DALProduct();
            int ProductIdd = obj.ProductDeleteCheck(ProductId);

            if (ProductIdd != 0)
            {
                MessageBox.Show("Item Have Dependent Records");

            }
            else
            { 
                DALProduct objj = new DALProduct();
                objj.DeleteProduct(ProductId);
                lst = new DALProduct().GetAllProducts();
                LVShowProduct.ItemsSource = lst;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        { 
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

            if (Patern.IsMatch(txtPrice.Text))
            {
                BEProduct be = new BEProduct();
                be.ProductId = ProductId;
                be.ProductName = txtProduct.Text;
                be.UnitPrice = Convert.ToDecimal(txtPrice.Text);
                DALProduct obj = new DALProduct();
                obj.UpdateProduct(be);
                txtProduct.Text = null;
                txtPrice.Text = null;
                btnAdd.Visibility = System.Windows.Visibility.Visible;
                btnUpdate.Visibility = System.Windows.Visibility.Hidden;
                lst = new DALProduct().GetAllProducts();
                LVShowProduct.ItemsSource = lst;
            }
        }

        public void ProductTypeDropDownFill()
        {

            cblProductType.ItemsSource = new DALProduct().GetAllProductsType();

            cblProductType.DisplayMemberPath = "TypeName";
            cblProductType.SelectedValuePath = "TypeId";
            cblProductType.SelectedValue = 1;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductTypeDropDownFill();
        }

    }
}

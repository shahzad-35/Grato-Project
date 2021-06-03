using BE;
using DAL;
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

namespace PointOfSaleW
{
    /// <summary>
    /// Interaction logic for RecipeList.xaml
    /// </summary>
    public partial class RecipeList : Window
    {
        public RecipeList()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }
        //action on window loaded

        List<BERecipeDef> lst = new List<BERecipeDef>();
        int ProductId;

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {

            lst = new DALRecipe().GetRecipe();
            LVShowRecipe.ItemsSource = lst;

        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //btnUpdate.Visibility = System.Windows.Visibility.Visible;
            //btnAdd.Visibility = System.Windows.Visibility.Hidden;
            //Button textBox = (Button)sender; 
            // ProductId = ((BE.BEProduct)textBox.DataContext).ProductId ; 
            //var GetRow = lst.FirstOrDefault(x => x.ProductId == ProductId); 
            // txtProduct.Text=GetRow.ProductName;
            // txtPrice.Text =Convert.ToString( GetRow.UnitPrice);


        }
         
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button textBox = (Button)sender;
           var RecipeId = ((BE.BERecipeDef)textBox.DataContext).RecipeId;
           
               DALRecipe objj = new DALRecipe();
               objj.DeleteRecipeDetailData(RecipeId);
                lst = new DALRecipe().GetRecipe();
                LVShowRecipe.ItemsSource = lst;
            
        }

        //    private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //    { 
        //        Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

        //        if (Patern.IsMatch(txtPrice.Text))
        //        {
        //            BEProduct be = new BEProduct();
        //            be.ProductId = ProductId;
        //            be.ProductName = txtProduct.Text;
        //            be.UnitPrice = Convert.ToDecimal(txtPrice.Text);
        //            DALProduct obj = new DALProduct();
        //            obj.ProductUpdate(be);
        //            txtProduct.Text = null;
        //            txtPrice.Text = null;
        //            btnAdd.Visibility = System.Windows.Visibility.Visible;
        //            btnUpdate.Visibility = System.Windows.Visibility.Hidden;
        //            lst = new DALProduct().ProductList();
        //            LVShowProduct.ItemsSource = lst;
        //        }
        //    }

        //}
    }
}

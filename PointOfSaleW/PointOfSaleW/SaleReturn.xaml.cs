using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SaleReturn.xaml
    /// </summary>
    public partial class SaleReturn : Window
    {
        public SaleReturn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

            if (Patern.IsMatch(txtSaleId.Text))
            {
                int Id =Convert.ToInt32( txtSaleId.Text);
                int SaleId = new DAL.DALSale().checkTokenId(Id);
                if (SaleId != 0)
                {
              int Result=  new DAL.DALSale().SaleReturn(Id);
             
                    MessageBox.Show("SuccessFully Returned");
             
                    //  Application.Current.MainWindow.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You Dont Have Permission to Return it");
                } 
            }
            else
            {
                MessageBox.Show("Please Enter Valid SaleID to Return");

            }
        }
    }
}

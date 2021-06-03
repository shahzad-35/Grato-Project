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
    /// Interaction logic for SaleEdit.xaml
    /// </summary>
    public partial class SaleEdit : Window
    {
        public SaleEdit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {  
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");
 
                if(Patern.IsMatch(txtSaleId.Text))
            {
                Global.SaleIdEdit = 0;
                Global.SaleIdEdit = Convert.ToInt32(txtSaleId.Text);
                int SaleId = new DAL.DALSale().checkTokenId(Global.SaleIdEdit);
                if (SaleId != 0)
                {
                    new SaleEditShow().Show();
                    SaleEdit Saleshow = new SaleEdit();
                    Saleshow.Close();
                  //  Application.Current.MainWindow.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No SaleID Found to Edit it.");
                }
                
            }
            else
            {
                MessageBox.Show("Please Enter Valid SaleID to Edit");
                    
            }
           
        }
    }
}

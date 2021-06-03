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
    /// Interaction logic for SaleEditByToken.xaml
    /// </summary>
    public partial class SaleEditByToken : Window
    {
        public SaleEditByToken()
        {
            InitializeComponent();
        }

        private void checkTokenId(object sender, RoutedEventArgs e)
        {
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

            if (Patern.IsMatch(txtTokenId.Text))
            {
                Global.TokenIdEdit = Convert.ToInt32(txtTokenId.Text);
                
                int tokenId = new DAL.DALSale().checkTokenId(Global.TokenIdEdit);
                if (tokenId == 0)
                {
                    MessageBox.Show("No Token ID found of this day");
                }
                else
                {
                    ///checking tokenId in orderQueue
                    tokenId = new DAL.DALSale().checkTokenIdOrderQueueTable(Global.TokenIdEdit);

                    if (tokenId == 0)
                    {
                        MessageBox.Show("Order has been delivered. You cannot change order at this moment.");
                    }
                    else
                    {
                        new SaleEditShow().Show();
                        SaleEditByToken obj = new SaleEditByToken();
                        obj.Close();
                        //  Application.Current.MainWindow.Close();
                    this.Close();
                    }
                
                }
                    showSaleEditShow();

                
            }
            else
            {
                MessageBox.Show("Please Enter Valid SaleID to Edit");
            }
        }

        private void showSaleEditShow()
        {


        }
    }
}

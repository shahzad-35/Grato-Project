using DAL;
using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for ReportCheck.xaml
    /// </summary>
    public partial class ReportCheck : Window
    {
        public ReportCheck()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DateFrom.Text == "")
            {
                errorMsgLabel.Content = "From Date is Required";
            }
            else
            {
                Global.DateFrom = null;
                Global.DateTo = null;
                Global.DateFrom = DateFrom.Text;
                Global.DateTo = DateTo.Text;
                new ReportCheckShow().Show();
            }
        }
    }
}

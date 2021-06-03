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
using Microsoft.Reporting.WinForms;

namespace PointOfSaleW
{
    /// <summary>
    /// Interaction logic for SaleReportByToken.xaml
    /// </summary>
    public partial class SaleReportByToken : Window
    {
        public SaleReportByToken()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string DateTo = Global.DateTo;
            if (DateTo != "")
            { 
               
                List<BE.BESale> BESaleReport_lIST = new DALSale().GetSaleByDate();

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "SaleReportByTokenDataSet"; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = BESaleReport_lIST;
                _reportViewer.LocalReport.ReportPath
                    = @"C:\Users\mpc\Documents\Visual Studio 2013\Projects\PointOfSaleW\PointOfSaleW\SaleReportByTokenRdlc.rdlc"; // Path of the rdlc file

                _reportViewer.LocalReport.DataSources.Add(reportDataSource);
                _reportViewer.RefreshReport();
            }

            else
            {
                
            }
            
        }
        private void reportViewer_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {

        }
    }
}

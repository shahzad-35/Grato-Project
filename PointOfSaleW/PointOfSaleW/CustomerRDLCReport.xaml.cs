using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for CustomerRDLCReport.xaml
    /// </summary>
    public partial class CustomerRDLCReport : Window
    {
        public CustomerRDLCReport()
        {
            InitializeComponent();
        }
         

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn ("ID",typeof(int)));
            //dt.Columns.Add(new DataColumn("Name", typeof(string)));
            //dt.Columns.Add(new DataColumn("City", typeof(string)));
            //dt.Columns.Add(new DataColumn("OrderAmount", typeof(int)));

            //DataRow dr = dt.NewRow();
            //dr["ID"] = 1;
            //dr["Name"] = "CK Nitin";
            //dr["City"] = "New York";
            //dr["OrderAmount"] = 100;
            //dt.Rows.Add(dr);


            List<BE.BESaleReport> BESaleReport_lIST = new DALSaleReport().SaleReportSelectById(Global.SaleId);

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = Global.CustomerDataSetName; // Name of the DataSet we set in .rdlc
            reportDataSource.Value = BESaleReport_lIST;
            _reportViewer.LocalReport.ReportPath = Global.CustomerRecietRdlcPath;
            //F:PointOfSaleW/PointOfSaleW/CustomerRecietRdlc.rdlc"; // Path of the rdlc file

            _reportViewer.LocalReport.DataSources.Add(reportDataSource);
            _reportViewer.RefreshReport();
        }

        private void reportViewer_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {

        }
    }
}

using BE;
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
    /// Interaction logic for ReportCheckShow.xaml
    /// </summary>
    public partial class ReportCheckShow : Window
    {
        public ReportCheckShow()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string DateTo = Global.DateTo;
            if (DateTo != "")
            {
                BEDailyReport obj = new BEDailyReport();
                obj.BranchId = Global.BranchId;
                obj.StartDate = Convert.ToDateTime(Global.DateFrom);
                obj.EndDate = Convert.ToDateTime(Global.DateTo);

                List<BE.BEDailyReport> BESaleReport_lIST = new DALSaleReport().DailyReportGetByDate(obj);

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = Global.CheckReportDataSetName; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = BESaleReport_lIST;
                _reportViewer.LocalReport.ReportPath = Global.CheckReportPath;// Path of the rdlc file

                _reportViewer.LocalReport.DataSources.Add(reportDataSource);
                _reportViewer.RefreshReport();
            }

            else
            {
                BEDailyReport obj = new BEDailyReport();
                obj.BranchId = Global.BranchId;
                obj.StartDate = Convert.ToDateTime(Global.DateFrom);

                List<BE.BEDailyReport> BESaleReport_lIST = new DALSaleReport().DailyReportGetByCurrentDate(obj);

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = Global.CheckReportDataSetName; // Name of the DataSet we set in .rdlc
                reportDataSource.Value = BESaleReport_lIST;
                _reportViewer.LocalReport.ReportPath = Global.CheckReportPath;

                _reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                _reportViewer.RefreshReport();
            }
            
        }
        private void reportViewer_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {

        }
    }
}

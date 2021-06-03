using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CONNECTION;
using BE;
using DAL;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;

namespace PointOfSaleW
{
    /// <summary>
    /// Interaction logic for RecentSale.xaml
    /// </summary>
    public partial class RecentSale : Window
    {
        public RecentSale()
        {
            InitializeComponent();
  Loaded += MyWindow_Loaded;
        }
        //action on window loaded

        List<BESale> lst = new List<BESale>();
        int ProductId;
            
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        { 
            lst = new DALSale().GetRecentSaleById();
            LVShowSale.ItemsSource = lst;
             
        }
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Button textBox = (Button)sender;
            int SaleId = ((BE.BESale)textBox.DataContext).SaleId;
            Global.SaleId = SaleId;
            ///Report
            ///
                List<BE.BESaleReport> BESaleReport_lIST = new DALSaleReport().SaleReportSelectById(Global.SaleId);

                LocalReport report = new LocalReport();
                report.ReportPath = Global.CustomerRecietRdlcPath;

                if (File.Exists(report.ReportPath))
                {
                    Console.WriteLine("FileExist");

                    var address = Directory.GetCurrentDirectory();
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = Global.CustomerDataSetName; //This refers to the dataset name in the RDLC file
                    rds.Value = BESaleReport_lIST;
                    report.DataSources.Add(rds);
                    Export(report);
                    Print(BESaleReport_lIST.Count);


                    //PrintDocument printDoc = new PrintDocument();
                    //printDoc.DocumentName = report.ReportPath;
                    ////  printDoc.PrinterSettings.PrintFileName = report.ReportPath;
                    //printDoc.Print();
                    new CustomerRDLCReport().Show();
                }
            
        }


        #region PrintReceiptRegion


        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        int pageHeight = 600;

        private Stream CreateStream(string name,
     string fileNameExtension, Encoding encoding,
     string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            try
            {

                string deviceInfo =
                @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3in</PageWidth>
                <PageHeight>4in</PageHeight>
                <MarginTop>-0.50in</MarginTop>
                <MarginLeft>0.05in</MarginLeft>
                <MarginRight>0.00in</MarginRight>
                <MarginBottom>0.00in</MarginBottom>
            </DeviceInfo>";
                Warning[] warnings;
                m_streams = new List<Stream>();
                report.Render("image", deviceInfo, CreateStream,
                   out warnings);



                foreach (Stream stream in m_streams)
                    stream.Position = 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
            ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
            ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
            ev.PageBounds.Width,
            pageHeight);

            //    Draw a white background for the report
            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            //m_currentPageIndex++;
            //ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print(int size)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                //if (size < 4)
                //    pageHeight = 280;
                //else if (size < 6)
                //    pageHeight = 450;
                //else if (size < 9)
                //    pageHeight = 500;

                pageHeight = 320;

                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }

        }


        #endregion
    }
}

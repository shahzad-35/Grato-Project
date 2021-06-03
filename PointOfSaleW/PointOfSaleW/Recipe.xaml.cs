using BE;
using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
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
    /// Interaction logic for Recipe.xaml
    /// </summary>

    public partial class Recipe : Window
    {
        public Recipe()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }

        //action on window loaded
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DropDownFill();
            
        }
        int ProductIddd;

        public void DropDownFill()
        {

            cblProduct.ItemsSource = new DALProduct().GetAllProducts();

            cblProduct.DisplayMemberPath = "ProductName";
            cblProduct.SelectedValuePath = "ProductId";
            cblProduct.SelectedValue = 1;

        }

        private void ddlProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
            //this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
            //txtUnits.Text = null;
            //txtTotalPrice.Text = null;
            //this.txtUnits.TextChanged += this.txtUnits_TextChanged;
            //this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;

        }

      
        List<BERecipeDetailList> items;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            items = Global.Recipeitems;
            if (cblProduct.SelectedIndex > 0 && !string.IsNullOrEmpty(txtRecipe.Text))
            {

                int id = (Convert.ToInt32(cblProduct.SelectedValue));


                if (Global.Recipeitems == null)
                {
                    items = new List<BERecipeDetailList>();
                    Global.Recipeitems = items;
                }

                items = (List<BERecipeDetailList>)Global.Recipeitems;
                int Productid = (Convert.ToInt32(cblProduct.SelectedValue));


                if (items != null && items.Count > 0)
                {
                    if (items.Exists(x => x.ProductId == Productid))
                    {
                        //foreach (var item in items)
                        //{
                        //    if (item.ProductId == Productid)
                        //    {
                        //        item.TotalPrice = item.TotalPrice + Math.Round(Convert.ToDecimal(txtTotalPrice.Text), 2);
                        //        item.TotalUnits = item.TotalUnits + Convert.ToDecimal(txtUnits.Text);
                        //    }
                        //}
                    }
                    else
                    {
                        var objBEProduct = (BEProduct)cblProduct.Items[cblProduct.SelectedIndex];
                        items.Add(new BERecipeDetailList()
                        {
                            RecipeName = txtRecipe.Text,
                            ProductId = (Convert.ToInt32(cblProduct.SelectedValue)),
                            ProductName = objBEProduct.ProductName,
                        });

                    }
                }
                else
                {
                    var objBEProduct = (BEProduct)cblProduct.Items[cblProduct.SelectedIndex];
                    items.Add(new BERecipeDetailList()
                    {
                        RecipeName = txtRecipe.Text,
                        ProductId = (Convert.ToInt32(cblProduct.SelectedValue)),
                        ProductName = objBEProduct.ProductName,
                    });
                }




                Global.Recipeitems = items;

                List<BEProduct> lst = new List<BEProduct>();
                lst = new DALProduct().GetAllProducts();

                LVShow.ItemsSource = items;
                LVShow.Items.Refresh();


                cblProduct.SelectedIndex = 0;

            }
        }
        

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            Button textBox = (Button)sender;
            ProductIddd = ((BE.BERecipeDetailList)textBox.DataContext).ProductId;

            items = (List<BE.BERecipeDetailList>)Global.Recipeitems;
            var item = items.First(x => x.ProductId == ProductIddd);
            items.Remove(item);
            Global.Recipeitems = items;   
            LVShow.ItemsSource = items;
            LVShow.Items.Refresh();
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (Global.Recipeitems != null)
            //&& Convert.ToInt32( txtNetPrice.Text)!=0
            {
                int Branch = Convert.ToInt32(Global.BranchId);
                items = (List<BERecipeDetailList>)Global.Recipeitems;

                BERecipeDef be = new BERecipeDef();
                
                be.RecipeName = txtRecipe.Text;
                int RecipeId = new DALRecipe().RecipeInsert(be);
                items = (List<BERecipeDetailList>)Global.Recipeitems;
                List<BERecipeDetail> list = new List<BERecipeDetail>();
                foreach (var item in items)
                {
                    list.Add(new BERecipeDetail
                    {
                        RecipeId = RecipeId,
                        ProductId = item.ProductId, 
                    });
                }

                new DALRecipe().SaveBulkRecipeDetail(list);

                Global.Recipeitems = null;

                Global.RecipeId = RecipeId; 
                LVShow.ItemsSource = null;
                txtRecipe.Text = "";
                LVShow.Items.Refresh();



                //List<BE.BESaleReport> BESaleReport_lIST = new DALSaleReport().SaleReportSelectById(Global.SaleId);

                //LocalReport report = new LocalReport();
                //report.ReportPath = @"F:\PointOfSaleW\PointOfSaleW\PointOfSaleW\CustomerRecietRdlc.rdlc";
                //if (File.Exists(report.ReportPath))
                //{
                //    string a = "";
                //}
                //var address = Directory.GetCurrentDirectory();
                //ReportDataSource rds = new ReportDataSource();
                //rds.Name = "CustomerDataSet";//This refers to the dataset name in the RDLC file
                //rds.Value = BESaleReport_lIST;
                //report.DataSources.Add(rds);
                //Export(report);
                //Print();
                //  PrintDocument printDoc = new PrintDocument();
                //  printDoc.DocumentName = report.ReportPath;
                ////  printDoc.PrinterSettings.PrintFileName = report.ReportPath;
                //  printDoc.Print();
                //   new CustomerRDLCReport().Show();
            }


        }

        #region MyRegion
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

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
            320);

            //    Draw a white background for the report
            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
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
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }

        }


        #endregion


       
        private void window_closing(object sender, ShutdownMode e)
        {
            if (MessageBox.Show("Do you want to LogOut?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Global.BranchId = 0;
                Global.Branch = null;
                Application.Current.Shutdown();
            }
            else
            {

            }

        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to LogOut?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Global.BranchId = 0;
                Global.Branch = null;
                Application.Current.Shutdown();
            }
            else
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RecipeList  cw = new RecipeList();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.Show();
        }
    }
}



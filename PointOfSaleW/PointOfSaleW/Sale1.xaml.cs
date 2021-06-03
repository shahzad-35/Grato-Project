
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

//using Microsoft.Reporting;
//using Microsoft.Reporting.WinForms;

//using Microsoft.ReportingServices.ReportProcessing;

namespace PointOfSaleW
{
    /// <summary>
    /// Interaction logic for Sale1.xaml
    /// </summary>
    public partial class Sale1 : Window
    {
        public Sale1()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }

        //action on window loaded
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DropDownFill();
           // hdnNetTotal.Visibility = Visibility.Hidden;

        }
        int ProductIddd;

        public void DropDownFill()
        {

            cblRecipe.ItemsSource = new DALRecipe().GetRecipe();

            cblRecipe.DisplayMemberPath = "RecipeName";
            cblRecipe.SelectedValuePath = "RecipeId";
            cblRecipe.SelectedValue = 1;

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
 
        //   // Also searches up the inheritance hierarchy
        //   private static FieldInfo GetStaticNonPublicFieldInfo(Type type, string name)
        //   {
        //       FieldInfo fi;
        //       do
        //       {
        //           fi = type.GetField(name, BindingFlags.Static | BindingFlags.NonPublic);
        //           type = type.BaseType;
        //       } while (fi == null && type != null);
        //       return fi;
        //   }


        //   private static void GetControlEventHandlerList(Control c)
        //   {
        //       Type type = c.GetType();
        //       PropertyInfo pi = type.GetProperty("EventHandlersStore",
        //          BindingFlags.NonPublic | BindingFlags.Instance);
        //       //return (EventHandlerList)pi.GetValue(c, null);
        //       object eventHandlersStore = pi.GetValue(c, null);

        //       var getRoutedEventHandlers = eventHandlersStore.GetType().GetMethod(
        //"GetRoutedEventHandlers", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //       var routedEventHandlers = (RoutedEventHandlerInfo[])getRoutedEventHandlers.Invoke(
        //           eventHandlersStore, new object[] { TextBox.TextChangedEvent });

        //       foreach (var routedEventHandler in routedEventHandlers)
        //           c.RemoveHandler(TextBox.TextChangedEvent, routedEventHandler.Handler);
        //   }

        //   private static object GetControlEventKey(Control c, string eventName)
        //   {
        //       Type type = c.GetType();
        //       FieldInfo eventKeyField = GetStaticNonPublicFieldInfo(type, "Event" + eventName);
        //       if (eventKeyField == null)
        //       {
        //           if (eventName.EndsWith("Changed"))
        //               eventKeyField = GetStaticNonPublicFieldInfo(type, "Event" + eventName.Remove(eventName.Length - 7)); // remove "Changed"
        //           else
        //               eventKeyField = GetStaticNonPublicFieldInfo(type, "EVENT_" + eventName.ToUpper());
        //           if (eventKeyField == null)
        //           {
        //               // Not all events in the WinForms controls use this pattern.
        //               // Other methods can be used to search for the event handlers if required.
        //               return null;
        //           }
        //       }
        //       return eventKeyField.GetValue(c);
        //   }


        //   private void RemoveClickEvent(TextBox b)
        //   {

        //       TextBox tb = (TextBox)b;

        //       GetControlEventHandlerList(tb);
        //       //FieldInfo f1 = typeof(Control).GetField("TextChanged",
        //       //    BindingFlags.Static | BindingFlags.NonPublic);
        //       //object obj = f1.GetValue(b);
        //       //PropertyInfo pi = b.GetType().GetProperty("Events",
        //       //    BindingFlags.NonPublic | BindingFlags.Instance);
        //       //EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
        //       //list.RemoveHandler(obj, list[obj]);
        //   }


        //   private void BindClickEvent(TextBox b)
        //   {
        //       //FieldInfo f1 = typeof(Control).GetField("TextChanged",
        //       //    BindingFlags.Static | BindingFlags.NonPublic);
        //       //object obj = f1.GetValue(b);
        //       //PropertyInfo pi = b.GetType().GetProperty("Events",
        //       //    BindingFlags.NonPublic | BindingFlags.Instance);
        //       //EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
        //       ////list.RemoveHandler(obj, list[obj]);
        //       //list.AddHandler(obj, list[obj]);
        //       TextBox tb = (TextBox)b;

        //       //var eventList = GetControlEventHandlerList(tb);
        //       //var eventKey = GetControlEventKey(tb, "TextChanged");
        //       //var handlers = eventList[eventKey];
        //       //// Reattach the handlers
        //       //eventList.AddHandler(eventKey, handlers);



        //   }

        private void txtDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

            if (Patern.IsMatch(txtDiscount.Text) && txtNetPrice.Text != "" && Convert.ToDecimal(txtNetPrice.Text) > Convert.ToDecimal(txtDiscount.Text))
            {
                if (!string.IsNullOrEmpty(txtDiscount.Text))
                {
                    lblDiscountMsg.Content = "";
                    decimal NetPrice = Convert.ToDecimal(txtNetPrice.Text);
                    decimal Discount = Convert.ToDecimal(txtDiscount.Text);
                    decimal FinalTotal = NetPrice - Discount;
                    txtNetTotal.Text = Convert.ToString(FinalTotal);
                }
            }
            else
            {
                txtNetTotal.Text = txtNetPrice.Text;
                lblDiscountMsg.Content = "* Invalid";
                lblDiscountMsg.Foreground = new SolidColorBrush(Colors.Red);
                txtDiscount.Text = null;
            }

        }
        List<BERecipeSaleList> lst = new List<BERecipeSaleList>();
        List<BESaleDetailList> items;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var RecipeId = (Convert.ToInt32(cblRecipe.SelectedValue));
            lst = new DALRecipe().GetRecipeById(RecipeId);
            LVShowRecipe.ItemsSource = lst;

            TextBox objTextbox = new TextBox();
            objTextbox.Name = "TextBox";
            objTextbox.Text = "TextBox" ;
            //objTextbox.Top = top;
            //panel1.Controls.Add(objTextbox);

            //items = Global.items;
            //if (cblRecipe.SelectedIndex > 0 )
            //{

            //    int id = (Convert.ToInt32(cblRecipe.SelectedValue)); 
            //    DALProduct obj = new DALProduct();
            //    decimal price = obj.ProductPriceSelectById(id);
            //  //  decimal totalprice = Math.Round((Convert.ToDecimal(txtUnits.Text) * price), 2);

            //    if (Global.items == null)
            //    {
            //        items = new List<BESaleDetailList>();
            //        Global.items = items;
            //    }

            //    items = (List<BESaleDetailList>)Global.items;
            //    int Productid = (Convert.ToInt32(cblRecipe.SelectedValue));


            //    if (items != null && items.Count > 0)
            //    {
            //        if (items.Exists(x => x.ProductId == Productid))
            //        {
            //            foreach (var item in items)
            //            {
            //                if (item.ProductId == Productid)
            //                {
            //                    item.TotalPrice = item.TotalPrice + Math.Round(Convert.ToDecimal(txtTotalPrice.Text), 2);
            //                    item.TotalUnits = item.TotalUnits + Convert.ToDecimal(txtUnits.Text);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            var objBEProduct = (BEProduct)cblRecipe.Items[cblRecipe.SelectedIndex];
            //            items.Add(new BESaleDetailList()
            //            {
            //                ProductId = (Convert.ToInt32(cblRecipe.SelectedValue)),
            //                ProductName = objBEProduct.ProductName,
            //                UnitPrice = price,
            //                TotalUnits = Convert.ToDecimal(txtUnits.Text),
            //                TotalPrice = totalprice,
            //            });
            //        }
            //    }
            //    else
            //    {
            //        var objBEProduct = (BEProduct)cblRecipe.Items[cblRecipe.SelectedIndex];
            //        items.Add(new BESaleDetailList()
            //        {
            //            ProductId = (Convert.ToInt32(cblRecipe.SelectedValue)),
            //            ProductName = objBEProduct.ProductName,
            //            UnitPrice = price,
            //            TotalUnits = Convert.ToDecimal(txtUnits.Text),
            //            TotalPrice = totalprice,
            //        });
            //    }




            //    decimal total = items.Sum(item => item.TotalPrice);


            //    txtNetPrice.Text = Convert.ToString(total);
            //    hdnNetTotal.Text = Convert.ToString(total);
            //    txtNetTotal.Text = Convert.ToString(total);
            //    Global.items = items;

            //    List<BEProduct> lst = new List<BEProduct>();
            //    lst = new DALProduct().ProductSelect();

            //    LVShow.ItemsSource = items;
            //    LVShow.Items.Refresh();


            //    cblRecipe.SelectedIndex = 0;
            //    this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
            //    this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
            //    txtUnits.Text = null;
            //    txtTotalPrice.Text = null;
            //    this.txtUnits.TextChanged += this.txtUnits_TextChanged;
            //    this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;

            //}

        }

        //private void Delete_Click(object sender, RoutedEventArgs e)
        //{

        //    Button textBox = (Button)sender;
        //    ProductIddd = ((BE.BESaleDetailList)textBox.DataContext).ProductId;

        //    items = (List<BE.BESaleDetailList>)Global.items;
        //    var item = items.First(x => x.ProductId == ProductIddd);
        //    items.Remove(item);
        //    Global.items = items;
        //    decimal TotalPrice = items.Sum(itemcc => itemcc.TotalPrice);
        //    txtNetPrice.Text = Convert.ToString(TotalPrice);
        //    txtNetTotal.Text = Convert.ToString(TotalPrice);
        //    txtDiscount.Text = "";
        //    LVShow.ItemsSource = items;
        //    LVShow.Items.Refresh();
        //}

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (Global.items != null)
            //&& Convert.ToInt32( txtNetPrice.Text)!=0
            {
                int Branch = Convert.ToInt32(Global.BranchId);
                items = (List<BESaleDetailList>)Global.items;
                decimal Discount;
                if (!decimal.TryParse(txtDiscount.Text, out Discount)) Discount = 0;

                decimal totalSale = items.Sum(itemcc => itemcc.TotalPrice);
                if (txtCashRecieved.Text == "")
                {
                    Global.CashRevieved = 0;
                    Global.CashReturn = 0;
                }
                else
                {
                    Global.CashRevieved = Convert.ToDecimal(txtCashRecieved.Text);
                    Global.CashReturn = (Convert.ToDecimal(txtCashRecieved.Text) - Convert.ToDecimal(txtNetTotal.Text));
                }
                BESale be = new BESale();
                be.BranchId = Branch;
                be.TotalSale = totalSale;
                be.Discount = Discount;
                be.NetSale = Convert.ToDecimal(txtNetTotal.Text);
                be.CashRecieved = Global.CashRevieved;
                be.CashReturn = Global.CashReturn;
                be.CreatedDate = DateTime.Now;
                int SaleId = new DALSale().SaleInsert(be);
                items = (List<BESaleDetailList>)Global.items;
                List<BESaleDetail> list = new List<BESaleDetail>();
                foreach (var item in items)
                {
                    list.Add(new BESaleDetail
                    {
                        SaleId = SaleId,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        TotalUnits = item.TotalUnits,
                        TotalPrice = item.TotalPrice,
                        CreatedDate = DateTime.Now
                    });
                }

                new DALSale().SaveBulkSaleDetail(list);

                Global.items = null;
                this.txtCashRecieved.TextChanged -= this.txtCashRecieved_TextChanged;
                this.txtDiscount.TextChanged -= this.txtDiscount_TextChanged;
                txtCashRecieved.Text = null;
                txtDiscount.Text = null;
               // txtTotalPrice.Text = null;
                this.txtCashRecieved.TextChanged += this.txtCashRecieved_TextChanged;
                this.txtDiscount.TextChanged += this.txtDiscount_TextChanged;

                txtCashReturn.Text = null;
               // txtTotalPrice.Text = null;
                txtNetTotal.Text = null;

                Global.SaleId = SaleId;
                txtNetPrice.Text = null;
                hdnNetTotal.Text = null;
                //LVShow.ItemsSource = null;
                //LVShow.Items.Refresh();



                List<BE.BESaleReport> BESaleReport_lIST = new DALSaleReport().SaleReportSelectById(Global.SaleId);

                LocalReport report = new LocalReport();
                report.ReportPath = @"F:\PointOfSaleW\PointOfSaleW\PointOfSaleW\CustomerRecietRdlc.rdlc";
                if (File.Exists(report.ReportPath))
                {
                    string a = "";
                }
                var address = Directory.GetCurrentDirectory();
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "CustomerDataSet";//This refers to the dataset name in the RDLC file
                rds.Value = BESaleReport_lIST;
                report.DataSources.Add(rds);
                Export(report);
                Print();
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

        private void txtTotalPricechanged(object sender, TextChangedEventArgs e)
        {
               var txtbox = (TextBox)sender;
               var abc = (ListView)LVShowRecipe;
               Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");
               var bbc = (BERecipeSaleList)abc.Items.CurrentItem;
               bbc.TotalPrice = 77;
               bbc.TotalUnits = 99;
               LVShowRecipe.SelectedValue = bbc;
               LVShowRecipe.Items.Refresh();

            //if (Patern.IsMatch(txtTotalPrice.Text))
            //{
           
                if (txtbox.Name == "txtTotalPrice" )
                {

                    TextBox textBox = (TextBox)sender;
                    decimal TotalPrice =Convert.ToDecimal( txtbox.Text);

                   // var UnitPrice = Convert.ToDecimal(((BE.BERecipeSaleList)textBox.DataContext).UnitPrice);
                   // decimal NetPrice = TotalPrice * UnitPrice;
                    
                 
                
            }
            else
            {
                //lblPrice.Content = "* Invalid";
                //lblPrice.Foreground = new SolidColorBrush(Colors.Red); 
                // txtUnits.Text = null;
                //txtTotalPrice.Text = null;
                } 
    }

           
    
        private void txtTotalUnitschanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCashRecieved_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

            if (Patern.IsMatch(txtCashRecieved.Text) && txtNetTotal.Text != "")
            {
                if (txtCashRecieved.Text != "")
                {
                    lblRecievedMsg.Content = "";
                    decimal CashReturn = (Convert.ToDecimal(txtCashRecieved.Text) - Convert.ToDecimal(txtNetTotal.Text));
                    txtCashReturn.Text = Convert.ToString(CashReturn);
                }
                else
                {

                    txtCashReturn.Text = "";

                }
            }
            else
            {
                lblRecievedMsg.Content = "* Invalid";
                lblRecievedMsg.Foreground = new SolidColorBrush(Colors.Red);
                this.txtCashRecieved.TextChanged -= this.txtCashRecieved_TextChanged;
                txtCashRecieved.Text = null;
                this.txtCashRecieved.TextChanged += this.txtCashRecieved_TextChanged;
                txtCashReturn.Text = null;
            }
        }

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
    }
}


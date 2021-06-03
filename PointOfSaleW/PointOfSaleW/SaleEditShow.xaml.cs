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
    /// Interaction logic for SaleEditShow.xaml
    /// </summary>
    
    public partial class SaleEditShow : Window
    {
        public SaleEditShow()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }

        //action on window loaded

        List<BESaleDetailEdit> lst = new List<BESaleDetailEdit>();
                            
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
            DropDownFill();
            hdnNetTotal.Visibility = Visibility.Hidden; 

            // by saleId
           //lst = new DALSale().GetSaleDetailDataByTokenIdEdit(Global.SaleIdEdit);
            lst = new DALSale().GetSaleDetailDataByTokenIdEdit(Global.TokenIdEdit);

            LVShow.ItemsSource = lst;
            Global.SaleDetailList = null;
            Global.SaleDetailList = lst;
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
            this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
            txtUnits.Text = null;
            txtTotalPrice.Text = null;
            this.txtUnits.TextChanged += this.txtUnits_TextChanged;
            this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;

        }

        private void txtTotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

            var txtbox = (TextBox)sender;
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,2})?$");

            //if (Patern.IsMatch(txtTotalPrice.Text))
            //{
            if (txtbox.Name == "txtTotalPrice" && (Patern.IsMatch(txtTotalPrice.Text)))
            {
                if (cblProduct.SelectedIndex != 0 && !string.IsNullOrEmpty(txtTotalPrice.Text))
                {
                    lblPrice.Content = "";
                    lblUnits.Content = "";
                    //unbinding event
                    this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                    int id = (Convert.ToInt32(cblProduct.SelectedValue));
                    DALProduct obj = new DALProduct();
                    decimal price = obj.ProductPriceSelectById(ProductId);
                    decimal TPrice = Convert.ToDecimal(txtTotalPrice.Text);
                   // decimal TotalUnits = Math.Round((TPrice / price), 3);
                    decimal TotalUnits = (TPrice / price);
                    txtUnits.Text = Convert.ToString(TotalUnits);
                    //binding event
                    this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                    // }
                }
            }
            else
            {
                lblPrice.Content = "* Invalid";
                lblPrice.Foreground = new SolidColorBrush(Colors.Red);
                this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                txtUnits.Text = null;
                txtTotalPrice.Text = null;
                this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
            }
        }

        private void txtUnits_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtbox = (TextBox)sender;
            Regex Patern = new Regex(@"^\d+(?:\.\d{0,3})?$");
            //if (Patern.IsMatch(txtUnits.Text))
            //{

            if (txtbox.Name == "txtUnits" && (Patern.IsMatch(txtUnits.Text)))
            {
                if (cblProduct.SelectedIndex != 0 && !string.IsNullOrEmpty(txtUnits.Text))
                {
                    lblUnits.Content = "";
                    lblPrice.Content = "";
                    //unBinding event
                    this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                    int id = (Convert.ToInt32(cblProduct.SelectedValue));
                    DALProduct obj = new DALProduct();
                    decimal price = obj.ProductPriceSelectById(ProductId);
                    decimal TUnits = Convert.ToDecimal(txtUnits.Text);
                    decimal TAmount = Math.Round((price * TUnits), 2);
                    txtTotalPrice.Text = Convert.ToString(TAmount);

                    this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                    //BindClickEvent(txtUnits);
                    // }
                }
            }
            else
            {
                lblUnits.Content = "* Invalid";
                lblUnits.Foreground = new SolidColorBrush(Colors.Red);
                this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                txtUnits.Text = null;
                txtTotalPrice.Text = null;
                this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
            }
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

        List<BESaleDetailEdit> items;
        int ProductId;
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            items = Global.SaleDetailList;
            //if (cblProduct.SelectedIndex > 0 && !string.IsNullOrEmpty(txtUnits.Text))
                if ( !string.IsNullOrEmpty(txtUnits.Text))
            {

                int id = (Convert.ToInt32(cblProduct.SelectedValue));

                DALProduct obj = new DALProduct();
                decimal price = obj.ProductPriceSelectById(id);
                decimal totalprice = Math.Round((Convert.ToDecimal(txtUnits.Text) * price), 2);

                if (Global.SaleDetailList == null)
                {
                    items = new List<BESaleDetailEdit>();
                    Global.SaleDetailList = items;
                }

                items = (List<BESaleDetailEdit>)Global.SaleDetailList;
                int Productid = (Convert.ToInt32(cblProduct.SelectedValue));


                if (items != null && items.Count > 0)
                {
                    if (items.Exists(x => x.ProductId == Productid))
                    {
                        foreach (var item in items)
                        {
                            if (item.ProductId == Productid)
                            {
                                item.TotalPrice = Math.Round(Convert.ToDecimal(txtTotalPrice.Text), 2);
                                item.TotalUnits =Convert.ToDecimal(txtUnits.Text);
                            }
                        }
                    }
                    else
                    {
                        var objBEProduct = (BEProduct)cblProduct.Items[cblProduct.SelectedIndex];
                        items.Add(new BESaleDetailEdit()
                        {
                            ProductId = (Convert.ToInt32(cblProduct.SelectedValue)),
                            ProductName = objBEProduct.ProductName,
                            UnitPrice = price,
                            TotalUnits = Convert.ToDecimal(txtUnits.Text),
                            TotalPrice = totalprice,
                        });
                    }
                }
                else
                {
                    var objBEProduct = (BEProduct)cblProduct.Items[cblProduct.SelectedIndex];
                    items.Add(new BESaleDetailEdit()
                    {
                        ProductId = (Convert.ToInt32(cblProduct.SelectedValue)),
                        ProductName = objBEProduct.ProductName,
                        UnitPrice = price,
                        TotalUnits = Convert.ToDecimal(txtUnits.Text),
                        TotalPrice = totalprice,
                    });
                }




                decimal total = items.Sum(item => item.TotalPrice);


                txtNetPrice.Text = Convert.ToString(total);
                hdnNetTotal.Text = Convert.ToString(total);
                txtNetTotal.Text = Convert.ToString(total);
                Global.SaleDetailList = items;

                List<BEProduct> lst = new List<BEProduct>();
                lst = new DALProduct().GetAllProducts();

                LVShow.ItemsSource = items;
                LVShow.Items.Refresh();


                cblProduct.SelectedIndex = 0;
                this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                txtUnits.Text = null;
                txtTotalPrice.Text = null;
                this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;

            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            Button textBox = (Button)sender;
            ProductIddd = ((BE.BESaleDetailEdit)textBox.DataContext).ProductId;

            items = (List<BE.BESaleDetailEdit>)Global.SaleDetailList;
            var item = items.First(x => x.ProductId == ProductIddd);
            items.Remove(item);
            Global.SaleDetailList = items;
            decimal TotalPrice = items.Sum(itemcc => itemcc.TotalPrice);
            txtNetPrice.Text = Convert.ToString(TotalPrice);
            txtNetTotal.Text = Convert.ToString(TotalPrice);
            txtDiscount.Text = "";
            LVShow.ItemsSource = items;
            LVShow.Items.Refresh();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
            this.cblProduct.SelectionChanged -= this.ddlProduct_SelectionChanged;

            Button textBox = (Button)sender;
            ProductId = ((BE.BESaleDetailEdit)textBox.DataContext).ProductId;
            var GetRow = lst.FirstOrDefault(x => x.ProductId == ProductId);
         //   txtUnits.Text = Convert.ToString(GetRow.UnitPrice);
            string Totalprice = Convert.ToString(GetRow.TotalPrice); ;
            string Units = Convert.ToString(GetRow.TotalUnits); ;
            txtTotalPrice.Text = Totalprice;
            txtUnits.Text = Units;
           cblProduct.Text = Convert.ToString(GetRow.ProductName);

           this.txtUnits.TextChanged += this.txtUnits_TextChanged;
           this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
           this.cblProduct.SelectionChanged += this.ddlProduct_SelectionChanged;

           
        }
        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (txtNetPrice.Text !="")
            {
                   if (Global.SaleDetailList != null)
            //&& Convert.ToInt32( txtNetPrice.Text)!=0
            {
                int Branch = Convert.ToInt32(Global.BranchId);
                items = (List<BESaleDetailEdit>)Global.SaleDetailList;
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
                       /// we are no longer editing invoice by sale id 
                       /// so changing from saleId to tokenId
                BESale be = new BESale();
                //be.SaleId = Global.SaleIdEdit;
                be.TokenId = Global.TokenIdEdit;
                be.BranchId = Branch;
                be.TotalSale = totalSale;
                be.Discount = Discount;
                be.NetSale = Convert.ToDecimal(txtNetTotal.Text);
                be.CashRecieved = Global.CashRevieved;
                be.CashReturn = Global.CashReturn;
                be.CreatedDate = DateTime.Now;
                int saleId = new DALSale().SaleInsertEditByTokenId(be);
                items = (List<BESaleDetailEdit>)Global.SaleDetailList;
                List<BESaleDetail> list = new List<BESaleDetail>();
                foreach (var item in items)
                {
                    list.Add(new BESaleDetail
                    {
                        SaleId = saleId,
                        TokenId = Global.TokenIdEdit,
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
                txtTotalPrice.Text = null;
                this.txtCashRecieved.TextChanged += this.txtCashRecieved_TextChanged;
                this.txtDiscount.TextChanged += this.txtDiscount_TextChanged;

                txtCashReturn.Text = null;
                txtTotalPrice.Text = null;
                txtNetTotal.Text = null;

                Global.SaleId = saleId;
                txtNetPrice.Text = null;
                hdnNetTotal.Text = null;
                LVShow.ItemsSource = null;
                LVShow.Items.Refresh();



                //List<BE.BESaleReport> BESaleReport_lIST = new DALSaleReport().SaleReportSelectById(Global.SaleId);

                //LocalReport report = new LocalReport();
                //report.ReportPath = @"D:/PointOfSaleW\PointOfSaleW\CustomerRecietRdlc.rdlc";
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



                this.Close();
               // Application.Current.MainWindow.Close();
                        
                //  PrintDocument printDoc = new PrintDocument();
                //  printDoc.DocumentName = report.ReportPath;
                ////  printDoc.PrinterSettings.PrintFileName = report.ReportPath;
                //  printDoc.Print();
                //   new CustomerRDLCReport().Show();
            }
            }
            else
            {
                MessageBox.Show("Please Make Some Changes to Checkout");
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           // Application.Current.MainWindow.Close();
        }
    }
}

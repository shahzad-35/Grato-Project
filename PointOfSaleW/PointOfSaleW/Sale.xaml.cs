
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
    public partial class Sale : Window
    {


        List<BESaleDetailList> saleDetailListItems;
        CustomerScreen customerScreenObj;


        public Sale()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DropDownFill();
            hdnNetTotal.Visibility = Visibility.Hidden;

            /// restoring TokenIDs to JalebiListView
            /// 
            JalebiListView.ItemsSource = Global.JalebiOrderList;
            JalebiListView.Items.Refresh();

            /// restoring TokenID to SamosaListView
            /// 
            SamosaListView.ItemsSource = Global.SamosaOrderList;
            SamosaListView.Items.Refresh();

            /// restoring TokenID to OtherListView        
            /// 
            OthersListView.ItemsSource = refreshOthersOrderList();
            OthersListView.Items.Refresh();


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

            orderLabel.Content = "";
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
                    int Productid = (Convert.ToInt32(cblProduct.SelectedValue));
                    DALProduct objj = new DALProduct();
                    decimal UnitPrice = objj.ProductPriceSelectById(Productid);

                    if (cblProduct.Text == "Samosa" || cblProduct.Text == "Chicken Samosa" || cblProduct.Text == "Plates" || cblProduct.Text == "Drink")
                    {
                        if (Convert.ToDecimal(txtTotalPrice.Text) % UnitPrice == 0)
                        {
                             lblPrice.Content = "";
                        lblUnits.Content = "";
                        //unbinding event
                        this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                        int id = (Convert.ToInt32(cblProduct.SelectedValue));
                        DALProduct obj = new DALProduct();
                        decimal price = obj.ProductPriceSelectById(id);
                        decimal TPrice = Convert.ToDecimal(txtTotalPrice.Text);
                        decimal TotalUnits = Math.Round((TPrice / price), 3);
                        txtUnits.Text = Convert.ToString(TotalUnits);
                        //binding event
                        this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                        }
                      
                        else
                        {
                            lblPrice.Content = "* Invalid";
                            lblPrice.Foreground = new SolidColorBrush(Colors.Red);

                            this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                            txtUnits.Text = null; 
                            this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                        }
                    }
                    else if (cblProduct.Text=="Kachori")
                    {
                        if (Convert.ToDecimal(txtTotalPrice.Text) % UnitPrice == 0)
                        {
                             lblPrice.Content = "";
                        lblUnits.Content = "";
                        //unbinding event
                        this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                        int id = (Convert.ToInt32(cblProduct.SelectedValue));
                        DALProduct obj = new DALProduct();
                        decimal price = obj.ProductPriceSelectById(id);
                        decimal TPrice = Convert.ToDecimal(txtTotalPrice.Text);
                        decimal TotalUnits = Math.Round((TPrice / price), 3);
                        txtUnits.Text = Convert.ToString(TotalUnits);
                        //binding event
                        this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                        }
                      
                        else
                        {
                            lblPrice.Content = "* Invalid";
                            lblPrice.Foreground = new SolidColorBrush(Colors.Red);

                            this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                            txtUnits.Text = null; 
                            this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                        }
                    }
                    else
                    {
                        lblPrice.Content = "";
                        lblUnits.Content = "";
                        //unbinding event
                        this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                        int id = (Convert.ToInt32(cblProduct.SelectedValue));
                        DALProduct obj = new DALProduct();
                        decimal price = obj.ProductPriceSelectById(id);
                        decimal TPrice = Convert.ToDecimal(txtTotalPrice.Text);
                        decimal TotalUnits = Math.Round((TPrice / price), 3);
                        txtUnits.Text = Convert.ToString(TotalUnits);
                        //binding event
                        this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                    }
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
                int Productid = (Convert.ToInt32(cblProduct.SelectedValue));
                DALProduct objj = new DALProduct();
                decimal UnitPrice = objj.ProductPriceSelectById(Productid);

                if (cblProduct.SelectedIndex != 0 && !string.IsNullOrEmpty(txtUnits.Text))
                { 
                    if (cblProduct.Text == "Samosa" ||  cblProduct.Text == "Chicken Samosa" || cblProduct.Text == "Plates" || cblProduct.Text == "Drink")
                    {
                        if ((UnitPrice * Convert.ToDecimal(txtUnits.Text)) % UnitPrice == 0)
                        { 
                            lblUnits.Content = "";
                            lblPrice.Content = "";
                            //unBinding event
                            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                            int id = (Convert.ToInt32(cblProduct.SelectedValue));
                            DALProduct obj = new DALProduct();
                            decimal price = obj.ProductPriceSelectById(id);
                            decimal TUnits = Convert.ToDecimal(txtUnits.Text);
                            decimal TAmount = Math.Round((price * TUnits), 2);
                            txtTotalPrice.Text = Convert.ToString(TAmount);

                            this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                            //BindClickEvent(txtUnits);
                            // }
                        }

                        else
                        {
                            lblPrice.Content = "* Invalid";
                            lblPrice.Foreground = new SolidColorBrush(Colors.Red);

                            this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                            txtUnits.Text = null;
                            this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                        }
                    }
                    else if (cblProduct.Text == "Kachori")
                    {
                        if ((UnitPrice * Convert.ToDecimal(txtUnits.Text)) % UnitPrice == 0)
                             
                        { 
                            lblUnits.Content = "";
                            lblPrice.Content = "";
                            //unBinding event
                            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                            int id = (Convert.ToInt32(cblProduct.SelectedValue));
                            DALProduct obj = new DALProduct();
                            decimal price = obj.ProductPriceSelectById(id);
                            decimal TUnits = Convert.ToDecimal(txtUnits.Text);
                            decimal TAmount = Math.Round((price * TUnits), 2);
                            txtTotalPrice.Text = Convert.ToString(TAmount);

                            this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                            //BindClickEvent(txtUnits);
                            // }
                        }

                        else
                        {
                            lblPrice.Content = "* Invalid";
                            lblPrice.Foreground = new SolidColorBrush(Colors.Red);

                            this.txtUnits.TextChanged -= this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                            txtUnits.Text = null;
                            this.txtUnits.TextChanged += this.txtUnits_TextChanged;
                            this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                        }
                    }
                    else
                    {  
                        lblUnits.Content = "";
                        lblPrice.Content = "";
                        //unBinding event
                        this.txtTotalPrice.TextChanged -= this.txtTotalPrice_TextChanged;
                        int id = (Convert.ToInt32(cblProduct.SelectedValue));
                        DALProduct obj = new DALProduct();
                        decimal price = obj.ProductPriceSelectById(id);
                        decimal TUnits = Convert.ToDecimal(txtUnits.Text);
                        decimal TAmount = Math.Round((price * TUnits), 2);
                        txtTotalPrice.Text = Convert.ToString(TAmount);

                        this.txtTotalPrice.TextChanged += this.txtTotalPrice_TextChanged;
                        //BindClickEvent(txtUnits);
                        // }
                    }
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

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            saleDetailListItems = Global.items;
            if (cblProduct.SelectedIndex > 0 && !string.IsNullOrEmpty(txtUnits.Text))
            {
                /// getting id of product from combo box.
                int productID = (Convert.ToInt32(cblProduct.SelectedValue));

                /// getting product price by its ID from database
                DALProduct obj = new DALProduct();
                int productPrice = obj.ProductPriceSelectById(productID);
                //decimal totalprice = Math.Round((Convert.ToDecimal(txtUnits.Text) * productPrice), 2);
                decimal tempTotalPrice = (Convert.ToDecimal(txtUnits.Text) * productPrice);
                int totalprice = Convert.ToInt32(tempTotalPrice);


                /// [Global.items] will be null when adding first product to the cart.
                /// 
                if (Global.items == null)
                {
                    saleDetailListItems = Global.items = new List<BESaleDetailList>();
                    //Global.items = items;
                }

                saleDetailListItems = (List<BESaleDetailList>)Global.items;

                

                /// when there is aready products in cart
                /// 
                if (saleDetailListItems != null && saleDetailListItems.Count > 0)
                {
                    if (saleDetailListItems.Exists(x => x.ProductId == productID))
                    {
                        foreach (var item in saleDetailListItems)
                        {
                            if (item.ProductId == productID)
                            {
                                item.TotalPrice = item.TotalPrice + Math.Round(Convert.ToDecimal(txtTotalPrice.Text), 2);
                                item.TotalUnits = item.TotalUnits + Convert.ToDecimal(txtUnits.Text);
                            }
                        }
                    }
                    else
                    {
                        var objBEProduct = (BEProduct)cblProduct.Items[cblProduct.SelectedIndex];
                        saleDetailListItems.Add(new BESaleDetailList()
                        {
                            ProductId = (Convert.ToInt32(cblProduct.SelectedValue)),
                            ProductName = objBEProduct.ProductName,
                            UnitPrice = productPrice,
                            TotalUnits = Convert.ToDecimal(txtUnits.Text),
                            TotalPrice = totalprice,
                        });
                    }
                }

                    /// adding first product in cart
                else
                {
                    var objBEProduct = (BEProduct)cblProduct.Items[cblProduct.SelectedIndex];
                    //Console.WriteLine(objBEProduct.ProductName);
                    saleDetailListItems.Add(new BESaleDetailList()
                    {
                        ProductId = (Convert.ToInt32(cblProduct.SelectedValue)),
                        ProductName = objBEProduct.ProductName,
                        UnitPrice = productPrice,
                        TotalUnits = Convert.ToDecimal(txtUnits.Text),
                        TotalPrice = totalprice,
                    });
                }




                decimal total = saleDetailListItems.Sum(item => item.TotalPrice);


                txtNetPrice.Text = Convert.ToString(total);
                hdnNetTotal.Text = Convert.ToString(total);
                txtNetTotal.Text = Convert.ToString(total);
                
                Global.items = saleDetailListItems;

                //List<BEProduct> lst = new List<BEProduct>();
                //lst = new DALProduct().ProductSelect();

                /// adding item to cart
                LVShow.ItemsSource = saleDetailListItems;
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
            ProductIddd = ((BE.BESaleDetailList)textBox.DataContext).ProductId;

            saleDetailListItems = (List<BE.BESaleDetailList>)Global.items;
            var item = saleDetailListItems.First(x => x.ProductId == ProductIddd);
            saleDetailListItems.Remove(item);
            Global.items = saleDetailListItems;
            decimal TotalPrice = saleDetailListItems.Sum(itemcc => itemcc.TotalPrice);
            txtNetPrice.Text = Convert.ToString(TotalPrice);
            txtNetTotal.Text = Convert.ToString(TotalPrice);
            txtDiscount.Text = "";
            LVShow.ItemsSource = saleDetailListItems;
            LVShow.Items.Refresh();
        }

        /// <summary>
        /// When click, order will be confirm.
        /// 
        /// Token No. will generate.
        /// Order will show on both screens.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (Global.items != null && txtCashRecieved.Text != "" && txtCashReturn.Text != "")
            //&& Convert.ToInt32( txtNetPrice.Text)!=0
            {
                try
                {
                    int cashReceived = Convert.ToInt32(txtCashRecieved.Text);
                    int cashReturn = Convert.ToInt32(txtCashReturn.Text);
                    if (cashReceived < Convert.ToDouble(txtNetTotal.Text))
                {
                    lblRecievedMsg.Content = "Please Collect " + txtNetTotal.Text;
                    return;
                }
                }
                catch(Exception ex){

                }
               
                int Branch = Convert.ToInt32(Global.BranchId);
                saleDetailListItems = (List<BESaleDetailList>)Global.items;
                decimal Discount;
                if (!decimal.TryParse(txtDiscount.Text, out Discount)) Discount = 0;

                decimal totalSale = saleDetailListItems.Sum(itemcc => itemcc.TotalPrice);
                
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

                /// inserting new order in SaleTable
                BESale be = new BESale();
                be.BranchId = Branch;
                be.TotalSale = totalSale;
                be.Discount = Discount;
                be.NetSale = Convert.ToDecimal(txtNetTotal.Text);
                be.CashRecieved = Global.CashRevieved;
                be.CashReturn = Global.CashReturn;
                be.CreatedDate = DateTime.Now;

                /// inserting new order
                /// 
                int SaleId = new DALSale().SaleInsert(be);

                Global.TokenId = new DALSale().GetLastTokenIdForSaleDetail();

                saleDetailListItems = (List<BESaleDetailList>)Global.items;
                List<BESaleDetail> list = new List<BESaleDetail>();

                initCustomerScreen();
                foreach (var item in saleDetailListItems)
                {
                    list.Add(new BESaleDetail
                    {
                        TokenId = Global.TokenId,
                        SaleId = SaleId,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        TotalUnits = item.TotalUnits,
                        TotalPrice = item.TotalPrice,
                        CreatedDate = DateTime.Now
                    });
                    ///getting TypeId from Product Table
                    ///
                    int typeId = new DALProduct().GetTypeIdFromProduct(item.ProductId);
                    
                    

                    /// adding order in list to show on screens
                    orderQueue(typeId);
                    
                    /// orderQueue insertion in database
                    new DALSale().OrderQueueInsert(Global.TokenId,typeId);
                }
                
                /// inserting new order in SaleDetail Table
                new DALSale().SaveBulkSaleDetail(list);

                

                /// clearing all fields
                ///
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
                

                Global.SaleId = SaleId;
                
                txtNetPrice.Text = null;
                hdnNetTotal.Text = null;
                LVShow.ItemsSource = null;
                LVShow.Items.Refresh();

                /// releasing CustomerScreen object
                /// 
                releaseCustomerScreen();
                orderLabel.Content = "Thank You. Order has been Confirmed.";


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
                    //new CustomerRDLCReport().Show();
                }
               
            }
            else
            {
                lblRecievedMsg.Content = "Please Collect Amount"; 
            }


        }


        private void btnEditSale_Click(object sender, RoutedEventArgs e)
        {
            SaleEdit cw = new SaleEdit();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.Show();

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //Global.items = null;
            //LVShow.ItemsSource = Global.items;
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

            txtNetPrice.Text = null;
            hdnNetTotal.Text = null;
            LVShow.ItemsSource = null;
            LVShow.Items.Refresh();

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

        private void btnReturnSale_Click(object sender, RoutedEventArgs e)
        {
            SaleReturn cw = new SaleReturn();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.Show();
        }

        private void btnSaleByToken_Click(object sender, RoutedEventArgs e)
        {
            SaleReportByToken sp = new SaleReportByToken();
            sp.Show();
        }


        private void btnEditSaleByToken_Click(object sender, RoutedEventArgs e)
        {
            SaleEditByToken obj = new SaleEditByToken();
            obj.ShowInTaskbar = false;
            obj.Owner = Application.Current.MainWindow;
            obj.Show();
        }

        private void btnTodaySale_Click(object sender, RoutedEventArgs e)
        {
            RecentSale cw = new RecentSale();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.Show();
        }

        #region OrderQueueRegion

        /// <summary>
        /// showing token number against order
        /// </summary>
        /// <param name="productTypeID"></param>
        private void orderQueue(int productTypeID)
        {
            //Console.WriteLine()
            switch (productTypeID)
            {
                case 1:
                    Global.JalebiOrderList.Add(Global.TokenId);
                    /// adding TokenID to JalebiListView
                    JalebiListView.ItemsSource = Global.JalebiOrderList;
                    JalebiListView.Items.Refresh();


                    if (customerScreenObj != null)
                        customerScreenObj.refreshJalebiQueue();
                    break;

                case 2:
                    /// adding TokenID to Samosa
                    Global.SamosaOrderList.Add(Global.TokenId);
                    SamosaListView.ItemsSource = Global.SamosaOrderList;
                    SamosaListView.Items.Refresh();

                    if (customerScreenObj != null)
                        customerScreenObj.refreshSamosaQueue();
                    break;
                case 5:
                    ///do nothing when hidden product is add
                    ///
                    /// we do not want to show these products in order queue list.
                    /// 
                    break;
                default:
                    /// adding TokenID to JalebiListView
                    Global.OtherOrderList.Add(new KeyValuePair<int, int>(Global.TokenId, productTypeID));
                    OthersListView.ItemsSource = refreshOthersOrderList();
                    OthersListView.Items.Refresh();

                    if (customerScreenObj != null)
                        customerScreenObj.refreshOthersQueue();
                    break;
            }

        }

        private List<int> refreshOthersOrderList()
        {
            return (from list in Global.OtherOrderList select list.Key).ToList();
        }

        private void JalebiListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (JalebiListView.SelectedIndex >= 0) 
            {
                
                if (MessageBox.Show("Do you want to remove from queue?", "Order Delivered?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    /// also delete entry from database
                    /// 
                    new DALSale().OrderQueueDelete(Global.JalebiOrderList[JalebiListView.SelectedIndex], 1);

                    /// from User end
                    /// 
                    Global.JalebiOrderList.RemoveAt(JalebiListView.SelectedIndex);
                    JalebiListView.Items.Refresh();

                    initCustomerScreen();
                    if (customerScreenObj != null)
                        customerScreenObj.refreshJalebiQueue();

                    releaseCustomerScreen();

                }
            }
        }

        private void SamosaListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SamosaListView.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Do you want to remove from queue?", "Order Delivered?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    /// also delete entry from database
                    /// 
                    new DALSale().OrderQueueDelete(Global.SamosaOrderList[SamosaListView.SelectedIndex], 2);

                    /// from User end
                    /// 
                    Global.SamosaOrderList.RemoveAt(SamosaListView.SelectedIndex);
                    SamosaListView.Items.Refresh();

                    initCustomerScreen();
                    if (customerScreenObj != null)
                        customerScreenObj.refreshSamosaQueue();

                    releaseCustomerScreen();
                }
            }
        }

        private void OthersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OthersListView.SelectedIndex >= 0)
            {
                //int productID = Global.OtherOrderList[Global.OtherOrderList.Keys.ElementAt(OthersListView.SelectedIndex)];

                
                if (MessageBox.Show("Do you want to remove from queue?", "Order Delivered?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    var id = Global.OtherOrderList[OthersListView.SelectedIndex];

                    /// also delete entry from database
                    /// 
                    new DALSale().OrderQueueDelete(id.Key,id.Value);

                   

                    //new DALSale().OrderQueueDelete(Global.OtherOrderList[OthersListView.SelectedIndex], productID);


                    /// from User end
                    /// 
                    Global.OtherOrderList.RemoveAt(OthersListView.SelectedIndex);
                    OthersListView.ItemsSource = refreshOthersOrderList();
                    OthersListView.Items.Refresh();

                    initCustomerScreen();
                    if (customerScreenObj != null)
                        customerScreenObj.refreshOthersQueue();

                    releaseCustomerScreen();
                }
            }
        }
            
        void initCustomerScreen()
        {
            /// initializing new value
            /// 
            customerScreenObj = Application.Current.Windows.OfType<CustomerScreen>().Take(1).SingleOrDefault();
        }
        void releaseCustomerScreen()
        {
            customerScreenObj = null;
        }

        #endregion



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




//                var pageSettings = new PageSettings();
//                pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
//                pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
//                pageSettings.Margins = report.GetDefaultPageSettings().Margins;

//                string deviceInfo =
//            @"<DeviceInfo>
//                                <OutputFormat>EMF</OutputFormat>
//                                <PageWidth>{pageSettings.PaperSize.Width * 100}in</PageWidth>
//                                <PageHeight>{pageSettings.PaperSize.Height * 100}in</PageHeight>
//                                <MarginTop>{pageSettings.Margins.Top * 100}in</MarginTop>
//                                <MarginLeft>{pageSettings.Margins.Left * 100}in</MarginLeft>
//                                <MarginRight>{pageSettings.Margins.Right * 100}in</MarginRight>
//                                <MarginBottom>{pageSettings.Margins.Bottom * 100}in</MarginBottom>
//                            </DeviceInfo>";
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

        private void JalebiClick(object sender, RoutedEventArgs e)
        {
            //cblProduct.Items

            //cblProduct.DisplayMemberPath = "ProductName";
            //cblProduct.SelectedValuePath = "ProductId";
            //cblProduct.SelectedValue = 3;

            cblProduct.SelectedIndex = 1;

        }

        private void SamosaClick(object sender, RoutedEventArgs e)
        {
            cblProduct.SelectedIndex = 2;
        }

        private void PakorayClick(object sender, RoutedEventArgs e)
        {
            cblProduct.SelectedIndex = 7;
        }
    }
}


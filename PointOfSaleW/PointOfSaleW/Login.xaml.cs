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

namespace PointOfSaleW
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        { 
            InitializeComponent();
         //Loaded += MyWindow_Loaded;
        }


        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {  

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = new DALLogin().BranchSelect().Where(x => x.BranchName == txtUserName.Text
             && x.Password == txtPassword.Password);

            var GetRow = data.FirstOrDefault(x => x.BranchName == txtUserName.Text);

            if (data.Any())
            {
                Global. Branch = txtUserName.Text;
                Global. BranchId = GetRow.BranchId;   //getting Branch id 
                Global.Password = txtPassword.Password;

                ///initializing order queue data
                LoadOrderQueueData(); 


                MainWindow obj = new MainWindow();
                obj.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                lblMsg.Content = "Incorrect UserName Or Password ";
            } 
        }


        /// <summary>
        /// whenever the application launches,
        /// initialize all order queue data.
        /// </summary>
        private void LoadOrderQueueData()
        {
            List<BEOrderQueue> list = new DALSale().OrderQueueGet();

            foreach (var singleItem in list)
            {
                switch (singleItem.TypeId)
                {
                    case 1:

                        Global.JalebiOrderList.Add(singleItem.TokenId);
                        break;
                    case 2:

                        Global.SamosaOrderList.Add(singleItem.TokenId);
                        break;

                    case 5:
                        //Hidden products
                        break;
                    default:

                        Global.OtherOrderList.Add(new KeyValuePair<int, int>(singleItem.TokenId, singleItem.TypeId));
                        break;
                }
            }


        }

    }
}

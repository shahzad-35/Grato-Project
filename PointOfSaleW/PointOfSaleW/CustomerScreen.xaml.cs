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
    /// Interaction logic for CustomerScreen.xaml
    /// </summary>
    public partial class CustomerScreen : Window
    {
        public CustomerScreen()
        {
            InitializeComponent();

        }

        private void OnLoaded_Screen(object sender, RoutedEventArgs e)
        {
            refreshJalebiQueue();
            refreshSamosaQueue();
            refreshOthersQueue();
        }

        internal void refreshJalebiQueue()
        {
            /// refresh JalebiListView
            JalebiListView.ItemsSource = Global.JalebiOrderList;
            JalebiListView.Items.Refresh();
        }
        internal void refreshSamosaQueue()
        {
            /// refresh SamosaListView
            SamosaListView.ItemsSource = Global.SamosaOrderList;
            SamosaListView.Items.Refresh();
        }
        internal void refreshOthersQueue()
        {
            /// refresh OthersListView
            /// 
            OthersListView.ItemsSource = (from list in Global.OtherOrderList select list.Key).ToList();
            OthersListView.Items.Refresh();


            //OthersListView.ItemsSource = Global.OtherOrderList;
        }



    }
}

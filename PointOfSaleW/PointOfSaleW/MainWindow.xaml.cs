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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PointOfSaleW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
        }

        
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.BranchId == 0)
            {
                Login winopen = new Login();
                winopen.Show();
                MainWindow winclose = new MainWindow();
                winclose.Close();
                Application.Current.MainWindow.Close();
            }

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Sale cw = new Sale();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.Show();
            //Sale WinOpen = new Sale();
            //WinOpen.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (Global.BranchId != 2)
            { 
                Product cw = new Product();
                cw.ShowInTaskbar = false;
                cw.Owner = Application.Current.MainWindow;
                cw.Show();
                //Product WinOpen = new Product();
                //WinOpen.Show();
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ReportCheck cw = new ReportCheck();
            cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.Show();
            //ReportCheck WinOpen = new ReportCheck();
            //WinOpen.Show();
        }


        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
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

        private void Customer_Screen_Click(object sender, RoutedEventArgs e)
        {
            CustomerScreen customerScreen = new CustomerScreen();
            customerScreen.ShowInTaskbar = false;
            customerScreen.Owner = Application.Current.MainWindow;
            customerScreen.Show();
        }

        private void MenuItem_Recipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe cw = new Recipe();
            //cw.ShowInTaskbar = false;
            cw.Owner = Application.Current.MainWindow;
            cw.Show();
            //ReportCheck WinOpen = new ReportCheck();
            //WinOpen.Show();
        }
    }
}

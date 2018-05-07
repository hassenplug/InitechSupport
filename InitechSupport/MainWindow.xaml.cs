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

namespace InitechSupport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CustomerDataClass CustomerData = new CustomerDataClass(@".\CustomerCallList.xml");
        public MainWindow()
        {
            this.DataContext = CustomerData;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CustomerData.SaveFile();
        }

        private void mnuFileAddEmail_Click(object sender, RoutedEventArgs e)
        {
            AddCall(tCallType.email);
        }

        private void mnuFileAddVoice_Click(object sender, RoutedEventArgs e)
        {
            AddCall(tCallType.voicemail);
        }

        private void mnuEditDestroy_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete all data?", "really?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CustomerData.CustomerList.Clear();
            }
        }

        private void mnuFileAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerListClass newcustomer = new CustomerListClass();
            CustomerData.CustomerList.Add(newcustomer);
            CustomerData.EditCustomer = true;
            lvCustomers.SelectedItem = newcustomer;
        }

        public void AddCall(tCallType callType)
        {
            CustomerListClass customer = (CustomerListClass)lvCustomers.SelectedItem;
            if (customer != null)
            { 
                customer.CallList.Add(new CallListClass { Source = callType });
            }
        }

        private void lvCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerData.EditCustomer)
            {
                ShowEditPanel(true);
            }
        }

        private void mnuEditShowCustomer_Unchecked(object sender, RoutedEventArgs e)
        {
            ShowEditPanel(false);
        }

        private void lvCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowEditPanel(true);
        }

        public void ShowEditPanel(bool show=true)
        {
            if (show)
            {
                grdRowCustomerPanel.Height = new GridLength(80);
            }
            else
            {
                grdRowCustomerPanel.Height = new GridLength(0);
            }

            CustomerData.EditCustomer = show;

        }

        private void mnuEditDeleteCall_Click(object sender, RoutedEventArgs e)
        {
            if (lvCalls.SelectedItem != null && lvCustomers.SelectedItem != null)
            {
                ((CustomerListClass)lvCustomers.SelectedItem).CallList.Remove((CallListClass)lvCalls.SelectedItem);
            }
        }

        private void mnuEditDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (lvCustomers.SelectedItem != null)
            {
                CustomerData.CustomerList.Remove((CustomerListClass)lvCustomers.SelectedItem);
            }
        }
    }
}

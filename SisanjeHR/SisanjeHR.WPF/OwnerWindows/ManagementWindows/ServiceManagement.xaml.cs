using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.AddNewWindows;
using SisanjeHrDesktopApp.OwnerWindows.OwnerCustom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.ManagementWindows
{
    /// <summary>
    /// Interaction logic for ServiceManagement.xaml
    /// </summary>
    public partial class ServiceManagement : Window
    {
        private readonly ServiceApiClient serviceApi;

        private readonly HairSalon hairSalon;
        
        public ServiceManagement(HairSalon hairSalon)
        {
            InitializeComponent();

            serviceApi = new ServiceApiClient();

            this.hairSalon = hairSalon;
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbServices.IsIndeterminate = true;
            await LoadServicesToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbServices.Visibility = Visibility.Hidden;
        }

        private async Task LoadServicesToStackPanel()
        {
            SpServices.Children.Clear();

            List<Service> services = await serviceApi.GetServices();

            if (services.Count > 0)
            {
                LblNoServices.Visibility = Visibility.Hidden;

                foreach (Service service in services)
                {
                    ServicePanel servicePanel = new ServicePanel(service, hairSalon);

                    SpServices.Children.Add(servicePanel);
                }
            }
            else
            {
                LblNoServices.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddNewService_Click(object sender, RoutedEventArgs e)
        {
            AddNewService addNewServiceWindow = new AddNewService();
            addNewServiceWindow.Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

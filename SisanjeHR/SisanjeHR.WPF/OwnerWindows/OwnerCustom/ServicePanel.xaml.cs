using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.EditWindows;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.OwnerWindows.OwnerCustom
{
    /// <summary>
    /// Interaction logic for ServicePanel.xaml
    /// </summary>
    public partial class ServicePanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly ServiceApiClient serviceApi;
        private readonly HairSalonServicesApiClient hairSalonServicesApi;

        private readonly Service service;
        private readonly HairSalon hairSalon;

        public ServicePanel(Service service, HairSalon hairSalon)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            serviceApi = new ServiceApiClient();
            hairSalonServicesApi = new HairSalonServicesApiClient();

            this.service = service;
            this.hairSalon = hairSalon;

            SetServiceValues();
        }

        private void SetServiceValues()
        {
            LblName.Text = service.Name;
            LblPrice.Text = service.Price.ToString();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            await AddServiceToHairSalon();
        }

        private async Task AddServiceToHairSalon()
        {
            bool success = await hairSalonServicesApi.AddServiceToHairSalon(hairSalon.Id, service);

            if (success)
            {
                MessageBox.Show("Successfully added service!");
            }
            else
            {
                MessageBox.Show("Hair Salon already contains this service!");
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditService editServiceWindow = new EditService(service);
            editServiceWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteService();
        }

        private async Task DeleteService()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await serviceApi.DeleteService(service.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail!");
            }
        }
    }
}

using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.ServiceViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditService.xaml
    /// </summary>
    public partial class EditService : Window
    {
        private readonly LoadingScreen ls;

        private readonly ServiceApiClient serviceApi;

        private readonly Service service;

        public EditService(Service service)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            serviceApi = new ServiceApiClient();

            this.service = service;

            SetServiceValues();
        }

        private void SetServiceValues()
        {
            TbName.Text = service.Name;
            TbPrice.Text = service.Price.ToString();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateService();
        }

        private async Task UpdateService()
        {
            if (ValidateInputs())
            {
                ServiceEditVM serviceEditVM = new ServiceEditVM()
                {
                    Id = service.Id,
                    Name = TbName.Text,
                    Price = decimal.Parse(TbPrice.Text)
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await serviceApi.UpdateService(serviceEditVM);
                ls.Close();

                if (success)
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("Fail!");
                }
            }
            else
            {
                MessageBox.Show("All input fields are required and price must be a number!");
            }
        }

        private bool ValidateInputs()
        {
            if (TbName.Text != string.Empty &&
                TbPrice.Text != string.Empty &&
               decimal.TryParse(TbPrice.Text, out var _))
            {
                return true;
            }
            return false;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

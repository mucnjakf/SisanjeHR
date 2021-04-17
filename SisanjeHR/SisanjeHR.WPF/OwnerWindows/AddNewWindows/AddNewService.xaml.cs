using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.ServiceViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewService.xaml
    /// </summary>
    public partial class AddNewService : Window
    {
        private readonly LoadingScreen ls;

        private readonly ServiceApiClient serviceApi;

        public AddNewService()
        {
            InitializeComponent();

            ls = new LoadingScreen();

            serviceApi = new ServiceApiClient();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertService();
        }

        private async Task InsertService()
        {
            if (ValidateInputs())
            {
                ServiceInsertVM serviceInsertVM = new ServiceInsertVM()
                {
                    Name = TbName.Text,
                    Price = decimal.Parse(TbPrice.Text)
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await serviceApi.InsertService(serviceInsertVM);
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

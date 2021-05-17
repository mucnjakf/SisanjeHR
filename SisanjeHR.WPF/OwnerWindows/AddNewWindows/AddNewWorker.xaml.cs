using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.WorkerViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewWorker.xaml
    /// </summary>
    public partial class AddNewWorker : Window
    {
        private readonly LoadingScreen ls;

        private readonly WorkerApiClient workerApi;

        private readonly Owner owner;

        public AddNewWorker(Owner owner)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            workerApi = new WorkerApiClient();

            this.owner = owner;
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertWorker();
        }

        private async Task InsertWorker()
        {
            if (ValidateInputs())
            {
                WorkerInsertVM workerInsertVM = new WorkerInsertVM()
                {
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    PhoneNumber = TbPhoneNumber.Text,
                    OwnerId = owner.Id                    
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await workerApi.InsertWorker(workerInsertVM);
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
                MessageBox.Show("All input fields are required and Password must match Confirm password!");
            }
        }

        private bool ValidateInputs()
        {
            if (TbUsername.Text != string.Empty &&
                TbPassword.Password != string.Empty &&
                TbFirstName.Text != string.Empty &&
                TbLastName.Text != string.Empty &&
                TbPhoneNumber.Text != string.Empty &&
                TbPassword.Password.Equals(TbConfirmPassword.Password))
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

using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.WorkerViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditWorker.xaml
    /// </summary>
    public partial class EditWorker : Window
    {
        private readonly LoadingScreen ls;

        private readonly WorkerApiClient workerApi;

        private readonly Worker worker;

        public EditWorker(Worker worker)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            workerApi = new WorkerApiClient();

            this.worker = worker;

            SetWorkerValues();
        }

        private void SetWorkerValues()
        {
            TbUsername.Text = worker.Username;
            TbFirstName.Text = worker.FirstName;
            TbLastName.Text = worker.LastName;
            TbPhoneNumber.Text = worker.PhoneNumber;
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateWorker();
        }

        private async Task UpdateWorker()
        {
            if (ValidateInputs())
            {
                WorkerEditVM workerEditVM = new WorkerEditVM()
                {
                    Id = worker.Id,
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    PhoneNumber = TbPhoneNumber.Text,
                    OwnerId = worker.OwnerId                   
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await workerApi.UpdateWorker(workerEditVM);
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
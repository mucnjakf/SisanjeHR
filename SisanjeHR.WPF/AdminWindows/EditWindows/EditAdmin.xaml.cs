using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.AdminViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditAdmin.xaml
    /// </summary>
    public partial class EditAdmin : Window
    {
        private readonly LoadingScreen ls;

        private readonly AdminApiClient adminApi;

        private readonly Admin admin;

        public EditAdmin(Admin admin)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            adminApi = new AdminApiClient();

            this.admin = admin;

            SetAdminValues();
        }

        private void SetAdminValues()
        {
            TbUsername.Text = admin.Username;
            TbFirstName.Text = admin.FirstName;
            TbLastName.Text = admin.LastName;
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateAdmin();
        }

        private async Task UpdateAdmin()
        {
            if (ValidateInputs())
            {
                AdminEditVM adminEditVM = new AdminEditVM()
                {
                    Id = admin.Id,
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await adminApi.UpdateAdmin(adminEditVM);
                ls.Close();

                if (success)
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed!");
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

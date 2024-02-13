using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.AdminViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewAdmin.xaml
    /// </summary>
    public partial class AddNewAdmin : Window
    {
        private readonly LoadingScreen ls;

        private readonly AdminApiClient adminApi;

        public AddNewAdmin()
        {
            InitializeComponent();

            ls = new LoadingScreen();

            adminApi = new AdminApiClient();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertAdmin();
        }

        private async Task InsertAdmin()
        {
            if (ValidateInputs())
            {
                AdminInsertVM adminInsertVM = new AdminInsertVM()
                {
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await adminApi.InsertAdmin(adminInsertVM);
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

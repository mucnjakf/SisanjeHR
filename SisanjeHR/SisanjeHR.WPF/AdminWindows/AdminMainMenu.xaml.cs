using DataAccessLayer.Models;
using SisanjeHrDesktopApp.AdminWindows.ManagementWindows;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows;
using Utilities.ViewModels.AdminViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows
{
    /// <summary>
    /// Interaction logic for AdminMainMenu.xaml
    /// </summary>
    public partial class AdminMainMenu : Window
    {
        private readonly AdminApiClient adminApi;

        public AdminMainMenu()
        {
            InitializeComponent();

            adminApi = new AdminApiClient();
        }

        public async Task SetAdminLoggedIn(AdminLoginVM adminLoginVM)
        {
            Admin admin = await adminApi.GetAdminByUidPwd(adminLoginVM);

            LblFirstName.Text = admin.FirstName;
            LblLastName.Text = admin.LastName;
        }

        private void BtnAdminManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(new AdminManagement());
        }

        private void BtnOwnerManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(new OwnerManagement());

        }

        private void BtnRegisteredUserManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(new RegisteredUserManagement());
        }

        private void OpenNewWindow(Window window)
        {
            window.Show();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Start startWindow = new Start();
            startWindow.Show();

            Close();
        }
    }
}

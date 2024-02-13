using DataAccessLayer.Models;
using SisanjeHrDesktopApp.AdminWindows.EditWindows;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.AdminWindows.AdminCustom
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly AdminApiClient adminApi;

        private readonly Admin admin;

        public AdminPanel(Admin admin)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            adminApi = new AdminApiClient();

            this.admin = admin;

            SetAdminValues();
        }

        private void SetAdminValues()
        {
            LblId.Text = admin.Id.ToString();
            LblFirstName.Text = admin.FirstName;
            LblLastName.Text = admin.LastName;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditAdmin editAdminWindow = new EditAdmin(admin);
            editAdminWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteAdmin();
        }

        private async Task DeleteAdmin()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await adminApi.DeleteAdmin(admin.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail!");
            }
        }
    }
}

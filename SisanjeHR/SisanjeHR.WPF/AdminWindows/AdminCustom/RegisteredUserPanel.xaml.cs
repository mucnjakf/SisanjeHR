using DataAccessLayer.Models;
using SisanjeHrDesktopApp.AdminWindows.EditWindows;
using SisanjeHrDesktopApp.AdminWindows.ShowWindows;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.AdminWindows.AdminCustom
{
    /// <summary>
    /// Interaction logic for RegisteredUserPanel.xaml
    /// </summary>
    public partial class RegisteredUserPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly RegisteredUserApiClient registeredUserApi;

        private readonly RegisteredUser registeredUser;

        public RegisteredUserPanel(RegisteredUser registeredUser)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            registeredUserApi = new RegisteredUserApiClient();

            this.registeredUser = registeredUser;

            SetRegisteredUserValues();
        }

        private void SetRegisteredUserValues()
        {
            LblFirstName.Text = registeredUser.FirstName;
            LblLastName.Text = registeredUser.LastName;
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowRegisteredUser showRegisteredUserWindow = new ShowRegisteredUser(registeredUser);
            showRegisteredUserWindow.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditRegisteredUser editRegisteredUserWindow = new EditRegisteredUser(registeredUser);
            editRegisteredUserWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteOwner();
        }

        private async Task DeleteOwner()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await registeredUserApi.DeleteRegisteredUser(registeredUser.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail!");
            }
        }
    }
}

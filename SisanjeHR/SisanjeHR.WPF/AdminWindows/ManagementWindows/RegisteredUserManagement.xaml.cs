using DataAccessLayer.Models;
using SisanjeHrDesktopApp.AdminWindows.AddNewWindows;
using SisanjeHrDesktopApp.AdminWindows.AdminCustom;
using ApiClient.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.ManagementWindows
{
    /// <summary>
    /// Interaction logic for RegisteredUserManagement.xaml
    /// </summary>
    public partial class RegisteredUserManagement : Window
    {
        private readonly RegisteredUserApiClient registeredUserApi;

        public RegisteredUserManagement()
        {
            InitializeComponent();

            registeredUserApi = new RegisteredUserApiClient();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbRegisteredUsers.IsIndeterminate = true;
            await LoadRegisteredUsersToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbRegisteredUsers.Visibility = Visibility.Hidden;
        }

        private async Task LoadRegisteredUsersToStackPanel()
        {
            SpRegisteredUsers.Children.Clear();

            List<RegisteredUser> registeredUsers = await registeredUserApi.GetRegisteredUsers();

            if (registeredUsers.Count > 0)
            {
                LblNoRegisteredUsers.Visibility = Visibility.Hidden;

                foreach (RegisteredUser registeredUser in registeredUsers)
                {
                    RegisteredUserPanel registeredUserPanel = new RegisteredUserPanel(registeredUser);
                    SpRegisteredUsers.Children.Add(registeredUserPanel);
                }
            }
            else
            {
                LblNoRegisteredUsers.Visibility = Visibility.Visible;
            }

        }

        private void BtnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAddNewRegisteredUser_Click(object sender, RoutedEventArgs e)
        {
            AddNewRegisteredUser addNewRegisteredUserWindow = new AddNewRegisteredUser();
            addNewRegisteredUserWindow.Show();
        }
    }
}

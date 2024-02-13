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
    /// Interaction logic for AdminManagement.xaml
    /// </summary>
    public partial class AdminManagement : Window
    {
        private readonly AdminApiClient adminApi;

        public AdminManagement()
        {
            InitializeComponent();

            adminApi = new AdminApiClient();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbAdmins.IsIndeterminate = true;
            await LoadAdminsToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbAdmins.Visibility = Visibility.Hidden;
        }

        private async Task LoadAdminsToStackPanel()
        {
            SpAdmins.Children.Clear();

            List<Admin> admins = await adminApi.GetAdmins();

            if (admins.Count > 1)
            {
                foreach (Admin admin in admins)
                {
                    LblNoAdmins.Visibility = Visibility.Hidden;

                    if (admin.Username != "master")
                    {
                        AdminPanel adminPanel = new AdminPanel(admin);
                        SpAdmins.Children.Add(adminPanel);
                    }
                }
            }
            else
            {
                LblNoAdmins.Visibility = Visibility.Visible;
            }            
        }

        private void BtnAddNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            AddNewAdmin addNewAdminWindow = new AddNewAdmin();
            addNewAdminWindow.Show();
        }

        private void BtnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

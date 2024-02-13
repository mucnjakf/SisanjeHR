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
    /// Interaction logic for OwnerManagement.xaml
    /// </summary>
    public partial class OwnerManagement : Window
    {
        private readonly OwnerApiClient ownersApi;

        public OwnerManagement()
        {
            InitializeComponent();

            ownersApi = new OwnerApiClient();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbOwners.IsIndeterminate = true;
            await LoadOwnersToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbOwners.Visibility = Visibility.Hidden;
        }

        private async Task LoadOwnersToStackPanel()
        {
            SpOwners.Children.Clear();

            List<Owner> owners = await ownersApi.GetOwners();

            if (owners.Count > 0)
            {
                foreach (Owner owner in owners)
                {
                    LblNoOwners.Visibility = Visibility.Hidden;

                    OwnerPanel ownerPanel = new OwnerPanel(owner);
                    SpOwners.Children.Add(ownerPanel);
                }
            }
            else
            {
                LblNoOwners.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddNewOwner_Click(object sender, RoutedEventArgs e)
        {
            AddNewOwner addNewOwnerWindow = new AddNewOwner();
            addNewOwnerWindow.Show();
        }

        private void BtnSubscriptions_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionsManagement subscriptionsManagementWindow = new SubscriptionsManagement();
            subscriptionsManagementWindow.Show();
        }

        private void BtnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

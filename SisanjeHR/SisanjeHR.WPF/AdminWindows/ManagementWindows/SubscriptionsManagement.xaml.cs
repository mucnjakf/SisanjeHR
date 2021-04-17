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
    /// Interaction logic for SubscriptionsManagement.xaml
    /// </summary>
    public partial class SubscriptionsManagement : Window
    {
        private readonly SubscriptionApiClient subscriptionApi;

        public SubscriptionsManagement()
        {
            InitializeComponent();

            subscriptionApi = new SubscriptionApiClient();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbSubscriptions.IsIndeterminate = true;
            await LoadSubscriptionsToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbSubscriptions.Visibility = Visibility.Hidden;
        }

        private async Task LoadSubscriptionsToStackPanel()
        {
            SpSubscriptions.Children.Clear();

            List<Subscription> subscriptions = await subscriptionApi.GetSubscriptions();

            if (subscriptions.Count > 0)
            {
                LblNoSubscriptions.Visibility = Visibility.Hidden;

                foreach (Subscription subscription in subscriptions)
                {
                    SubscriptionPanel subscriptionPanel = new SubscriptionPanel(subscription);

                    SpSubscriptions.Children.Add(subscriptionPanel);
                }
            }
            else
            {
                LblNoSubscriptions.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddNewSubscription_Click(object sender, RoutedEventArgs e)
        {
            AddNewSubscription addNewSubscriptionWindow = new AddNewSubscription();
            addNewSubscriptionWindow.Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

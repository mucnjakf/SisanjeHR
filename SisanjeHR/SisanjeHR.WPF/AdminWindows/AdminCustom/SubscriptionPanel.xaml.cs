using DataAccessLayer.Models;
using SisanjeHrDesktopApp.AdminWindows.EditWindows;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.AdminWindows.AdminCustom
{
    /// <summary>
    /// Interaction logic for SubscriptionPanel.xaml
    /// </summary>
    public partial class SubscriptionPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly SubscriptionApiClient subscriptionApi;

        private readonly Subscription subscription;

        public SubscriptionPanel(Subscription subscription)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            subscriptionApi = new SubscriptionApiClient();

            this.subscription = subscription;

            SetSubscriptionValues();
        }

        private void SetSubscriptionValues()
        {
            LblType.Text = subscription.Type;
            LblPrice.Text = subscription.Price.ToString();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditSubscription editSubscriptionWindow = new EditSubscription(subscription);
            editSubscriptionWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteSubscription();
        }

        private async Task DeleteSubscription()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await subscriptionApi.DeleteSubscription(subscription.Id);
            ls.Close();

            if (success==false)
            {
                MessageBox.Show("Fail!");
            }
        }
    }
}

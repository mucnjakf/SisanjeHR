using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.SubscriptionViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewSubscription.xaml
    /// </summary>
    public partial class AddNewSubscription : Window
    {
        private readonly LoadingScreen ls;

        private readonly SubscriptionApiClient subscriptionApi;

        public AddNewSubscription()
        {
            InitializeComponent();

            ls = new LoadingScreen();

            subscriptionApi = new SubscriptionApiClient();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertSubscription();
        }

        private async Task InsertSubscription()
        {
            if (ValidateInputs())
            {
                SubscriptionInsertVM subscriptionInsertVM = new SubscriptionInsertVM()
                {
                    Type = TbType.Text,
                    Price = decimal.Parse(TbPrice.Text)
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await subscriptionApi.InsertSubscription(subscriptionInsertVM);
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
                MessageBox.Show("All input fields are required and price must be a number!");
            }
        }

        private bool ValidateInputs()
        {
            if (TbType.Text != string.Empty &&
                TbPrice.Text != string.Empty &&
                decimal.TryParse(TbPrice.Text, out var _))
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

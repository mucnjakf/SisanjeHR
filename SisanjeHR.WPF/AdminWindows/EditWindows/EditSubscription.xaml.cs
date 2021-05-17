using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.SubscriptionViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditSubscription.xaml
    /// </summary>
    public partial class EditSubscription : Window
    {
        private readonly LoadingScreen ls;

        private readonly SubscriptionApiClient subscriptionApi;

        private readonly Subscription subscription;

        public EditSubscription(Subscription subscription)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            subscriptionApi = new SubscriptionApiClient();

            this.subscription = subscription;

            SetSubscriptionValues();
        }

        private void SetSubscriptionValues()
        {
            TbType.Text = subscription.Type;
            TbPrice.Text = subscription.Price.ToString();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateSubscription();
        }

        private async Task UpdateSubscription()
        {
            if (ValidateInputs())
            {
                SubscriptionEditVM subscriptionEditVM = new SubscriptionEditVM()
                {
                    Id = subscription.Id,
                    Type = TbType.Text,
                    Price = decimal.Parse(TbPrice.Text)
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await subscriptionApi.UpdateSubscription(subscriptionEditVM);
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

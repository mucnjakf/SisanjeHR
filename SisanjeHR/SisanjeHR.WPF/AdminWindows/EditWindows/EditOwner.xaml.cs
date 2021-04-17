using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.OwnerViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditOwner.xaml
    /// </summary>
    public partial class EditOwner : Window
    {
        private readonly LoadingScreen ls;

        private readonly SubscriptionApiClient subscriptionApi;
        private readonly OwnerApiClient ownerApi;

        private readonly Owner owner;

        public EditOwner(Owner owner)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            subscriptionApi = new SubscriptionApiClient();
            ownerApi = new OwnerApiClient();

            this.owner = owner;

            SetOwnerValues();
        }

        private async void SetOwnerValues()
        {
            TbUsername.Text = owner.Username;
            TbEmail.Text = owner.Email;
            TbFirstName.Text = owner.FirstName;
            TbLastName.Text = owner.LastName;
            TbPin.Text = owner.Pin;
            CbSubscriptions.ItemsSource = await subscriptionApi.GetSubscriptions();
            CbSubscriptions.Text = owner.Subscription.ToString();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateOwner();
        }

        private async Task UpdateOwner()
        {
            if (ValidateInputs())
            {
                OwnerEditVM ownerEditVM = new OwnerEditVM()
                {
                    Id = owner.Id,
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    Email = TbEmail.Text,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    Pin = TbPin.Text,
                    Subscription = (Subscription)CbSubscriptions.SelectedItem
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await ownerApi.UpdateOwner(ownerEditVM);
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
                MessageBox.Show("All input fields are required and Password must match Confirm password!");
            }
        }

        private bool ValidateInputs()
        {
            if (TbUsername.Text != string.Empty &&
                TbPassword.Password != string.Empty &&
                TbEmail.Text != string.Empty &&
                TbFirstName.Text != string.Empty &&
                TbLastName.Text != string.Empty &&
                TbPin.Text != string.Empty &&
                TbPassword.Password.Equals(TbConfirmPassword.Password) &&
                CbSubscriptions.SelectedItem != null)
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

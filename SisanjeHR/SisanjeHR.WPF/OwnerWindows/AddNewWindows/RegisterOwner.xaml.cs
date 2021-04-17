using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for RegisterOwner.xaml
    /// </summary>
    public partial class RegisterOwner : Window
    {
        private readonly LoadingScreen ls;

        private readonly SubscriptionApiClient subscriptionApi;
        private readonly OwnerApiClient ownerApi;

        private bool registrationSuccess = false;

        public RegisterOwner()
        {
            InitializeComponent();

            ls = new LoadingScreen();

            subscriptionApi = new SubscriptionApiClient();
            ownerApi = new OwnerApiClient();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadSubscriptionsToComboBox();
        }

        private async Task LoadSubscriptionsToComboBox()
        {
            List<Subscription> subscriptions = await subscriptionApi.GetSubscriptions();
            CbSubscriptions.ItemsSource = subscriptions;
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await RegisteredUser();
        }

        private async Task RegisteredUser()
        {
            if (ValidateInputs())
            {
                OwnerInsertVM ownerInsertVM = new OwnerInsertVM()
                {
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    Email = TbEmail.Text,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    Pin = TbPin.Text,
                    Subscription = (Subscription)CbSubscriptions.SelectedItem
                };

                ls.LblLoading.Text = "Registering";
                ls.Show();
                bool success = await ownerApi.InsertOwner(ownerInsertVM);
                ls.Close();

                if (success)
                {
                    registrationSuccess = true;
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

        public string GetRegisteredOwnerUsername()
        {
            return TbUsername.Text;
        }

        public string GetRegisteredOwnerPassword()
        {
            return TbPassword.Password;
        }

        public bool IsRegistrationSuccessfull()
        {
            return registrationSuccess;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }        
    }
}

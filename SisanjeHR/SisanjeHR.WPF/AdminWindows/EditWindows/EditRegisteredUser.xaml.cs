using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.RegisteredUserViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditRegisteredUser.xaml
    /// </summary>
    public partial class EditRegisteredUser : Window
    {
        private readonly LoadingScreen ls;

        private readonly CountryApiClient countryApi;
        private readonly RegisteredUserApiClient registeredUserApi;

        private readonly RegisteredUser registeredUser;

        public EditRegisteredUser(RegisteredUser registeredUser)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            countryApi = new CountryApiClient();
            registeredUserApi = new RegisteredUserApiClient();

            this.registeredUser = registeredUser;

            SetRegisteredUserValues();
        }

        private async void SetRegisteredUserValues()
        {
            TbUsername.Text = registeredUser.Username;
            TbFirstName.Text = registeredUser.FirstName;
            TbLastName.Text = registeredUser.LastName;
            TbEmail.Text = registeredUser.Email;
            TbAddress.Text = registeredUser.Location.Address;
            TbCity.Text = registeredUser.Location.City.Name;
            CbCountries.ItemsSource = await countryApi.GetCountries();
            CbCountries.Text = registeredUser.Location.City.Country.ToString();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateRegisteredUser();
        }

        private async Task UpdateRegisteredUser()
        {
            if (ValidateInputs())
            {
                RegisteredUserEditVM registeredUserEditVM = new RegisteredUserEditVM()
                {
                    Id = registeredUser.Id,
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    Email = TbEmail.Text,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    Address = TbAddress.Text,
                    City = TbCity.Text,
                    Country = (Country)CbCountries.SelectedItem
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await registeredUserApi.UpdateRegisteredUser(registeredUserEditVM);
                ls.Close();

                if (success)
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("Fail");
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
                TbAddress.Text != string.Empty &&
                TbCity.Text != string.Empty &&
                TbPassword.Password.Equals(TbConfirmPassword.Password) &&
                CbCountries.SelectedItem != null)
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

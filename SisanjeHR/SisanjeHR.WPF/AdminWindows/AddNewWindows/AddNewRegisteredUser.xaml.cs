using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.RegisteredUserViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewRegisteredUser.xaml
    /// </summary>
    public partial class AddNewRegisteredUser : Window
    {
        private readonly LoadingScreen ls;

        private readonly CountryApiClient countryApi;
        private readonly RegisteredUserApiClient registeredUserApi;

        public AddNewRegisteredUser()
        {
            InitializeComponent();

            ls = new LoadingScreen();

            countryApi = new CountryApiClient();
            registeredUserApi = new RegisteredUserApiClient();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCountriesToComboBox();
        }

        private async Task LoadCountriesToComboBox()
        {
            List<Country> countries = await countryApi.GetCountries();
            CbCountries.ItemsSource = countries;
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertRegisteredUser();
        }

        private async Task InsertRegisteredUser()
        {
            if (ValidateInputs())
            {
                RegisteredUserInsertVM registeredUserInsertVM = new RegisteredUserInsertVM()
                {
                    Username = TbUsername.Text,
                    Password = TbPassword.Password,
                    Email = TbEmail.Text,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    Address = TbAddress.Text,
                    City = TbCity.Text,
                    Country = (Country)CbCountries.SelectedItem
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await registeredUserApi.InsertRegisteredUser(registeredUserInsertVM);
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

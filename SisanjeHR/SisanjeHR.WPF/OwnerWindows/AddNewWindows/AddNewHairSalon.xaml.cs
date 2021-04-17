using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.HairSalonViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewHairSalon.xaml
    /// </summary>
    public partial class AddNewHairSalon : Window
    {
        private readonly LoadingScreen ls;

        private readonly CountryApiClient countryApi;
        private readonly HairSalonApiClient hairSalonApi;

        private readonly Owner owner;

        public AddNewHairSalon(Owner owner)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            countryApi = new CountryApiClient();
            hairSalonApi = new HairSalonApiClient();

            this.owner = owner;
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
            await InsertHairSalon();
        }

        private async Task InsertHairSalon()
        {
            if (ValidateInputs())
            {
                HairSalonInsertVM hairSalonInsertVM = new HairSalonInsertVM()
                {
                    Name = TbName.Text,
                    Description = TbDescription.Text,
                    Address = TbAddress.Text,
                    City = TbCity.Text,
                    Country = (Country)CbCountries.SelectedItem,
                    OwnerId = owner.Id
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await hairSalonApi.InsertHairSalon(hairSalonInsertVM);
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
                MessageBox.Show("All input fields are required!");
            }
        }

        private bool ValidateInputs()
        {
            if (TbName.Text != string.Empty &&
                TbDescription.Text != string.Empty &&
                TbAddress.Text != string.Empty &&
                TbCity.Text != string.Empty &&
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

using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.HairSalonViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditHairSalon.xaml
    /// </summary>
    public partial class EditHairSalon : Window
    {
        private readonly LoadingScreen ls;

        private readonly CountryApiClient countryApi;
        private readonly HairSalonApiClient hairSalonApi;

        private readonly HairSalon hairSalon;

        public EditHairSalon(HairSalon hairSalon)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            countryApi = new CountryApiClient();
            hairSalonApi = new HairSalonApiClient();

            this.hairSalon = hairSalon;

            SetHairSalonValues();
        }

        private async void SetHairSalonValues()
        {
            TbName.Text = hairSalon.Name;
            TbDescription.Text = hairSalon.Description;
            TbAddress.Text = hairSalon.Location.Address;
            TbCity.Text = hairSalon.Location.City.Name;
            CbCountries.ItemsSource = await countryApi.GetCountries();
            CbCountries.Text = hairSalon.Location.City.Country.ToString();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateHairSalon();
        }

        private async Task UpdateHairSalon()
        {
            if (ValidateInputs())
            {
                HairSalonEditVM hairSalonEditVM = new HairSalonEditVM()
                {
                    Id = hairSalon.Id,
                    Name = TbName.Text,
                    Description = TbDescription.Text,
                    Address = TbAddress.Text,
                    City = TbCity.Text,
                    Country = (Country)CbCountries.SelectedItem,
                    OwnerId = hairSalon.OwnerId
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await hairSalonApi.UpdateHairSalon(hairSalonEditVM);
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
                MessageBox.Show("All input fields are required");
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

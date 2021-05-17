using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.EditWindows;
using SisanjeHrDesktopApp.OwnerWindows.ShowWindows;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.OwnerWindows.OwnerCustom
{
    /// <summary>
    /// Interaction logic for HairSalonPanel.xaml
    /// </summary>
    public partial class HairSalonPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly HairSalonApiClient hairSalonApi;

        private readonly HairSalon hairSalon;

        public HairSalonPanel(HairSalon hairSalon)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            hairSalonApi = new HairSalonApiClient();

            this.hairSalon = hairSalon;

            SetHairSalonValues();
        }

        private void SetHairSalonValues()
        {
            LblName.Text = hairSalon.Name;
            LblLocation.Text = hairSalon.Location.Address;
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHairSalon showHairSalonWindow = new ShowHairSalon(hairSalon);
            showHairSalonWindow.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditHairSalon editHairSalonWindow = new EditHairSalon(hairSalon);
            editHairSalonWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteHairSalon();
        }

        private async Task DeleteHairSalon()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await hairSalonApi.DeleteHairSalon(hairSalon.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail");
            }
        }
    }
}

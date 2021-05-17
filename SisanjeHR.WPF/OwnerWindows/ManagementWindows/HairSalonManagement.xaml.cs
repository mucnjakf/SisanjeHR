using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.AddNewWindows;
using SisanjeHrDesktopApp.OwnerWindows.OwnerCustom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.ManagementWindows
{
    /// <summary>
    /// Interaction logic for HairSalonManagement.xaml
    /// </summary>
    public partial class HairSalonManagement : Window
    {
        private readonly HairSalonApiClient hairSalonApi;

        private readonly Owner owner;

        public HairSalonManagement(Owner owner)
        {
            InitializeComponent();

            hairSalonApi = new HairSalonApiClient();

            this.owner = owner;
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbHairSalons.IsIndeterminate = true;
            await LoadHairSalonsToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbHairSalons.Visibility = Visibility.Hidden;
        }

        private async Task LoadHairSalonsToStackPanel()
        {
            SpHairSalons.Children.Clear();

            List<HairSalon> hairSalons = await hairSalonApi.GetHairSalons(owner.Id);

            if (hairSalons.Count > 0)
            {
                foreach (HairSalon hairSalon in hairSalons)
                {
                    LblNoHairSalons.Visibility = Visibility.Hidden;

                    HairSalonPanel hairSalonPanel = new HairSalonPanel(hairSalon);

                    SpHairSalons.Children.Add(hairSalonPanel);
                }
            }
            else
            {
                LblNoHairSalons.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddNewHairSalon_Click(object sender, RoutedEventArgs e)
        {
            AddNewHairSalon addNewHairSalonWindow = new AddNewHairSalon(owner);
            addNewHairSalonWindow.Show();
        }

        private void BtnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

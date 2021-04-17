using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.ManagementWindows;
using SisanjeHrDesktopApp.OwnerWindows.OwnerCustom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.ShowWindows
{
    /// <summary>
    /// Interaction logic for ShowHairSalon.xaml
    /// </summary>
    public partial class ShowHairSalon : Window
    {
        private readonly OwnerApiClient ownerApi;
        private readonly HairSalonMethodsOfPaymentApiClient hairSalonMethodsOfPaymentApi;
        private readonly HairSalonWorkingHoursApiClient hairSalonWorkingHoursApi;
        private readonly WorkerApiClient workerApi;
        private readonly HairSalonServicesApiClient hairSalonServicesApi;

        private readonly HairSalon hairSalon;

        public ShowHairSalon(HairSalon hairSalon)
        {
            InitializeComponent();

            ownerApi = new OwnerApiClient();
            hairSalonMethodsOfPaymentApi = new HairSalonMethodsOfPaymentApiClient();
            hairSalonWorkingHoursApi = new HairSalonWorkingHoursApiClient();
            workerApi = new WorkerApiClient();
            hairSalonServicesApi = new HairSalonServicesApiClient();

            this.hairSalon = hairSalon;

            SetHairSalonValues();
        }

        private async void SetHairSalonValues()
        {            
            LblId.Text = hairSalon.Id.ToString();
            LblName.Text = hairSalon.Name;
            LblDescription.Text = hairSalon.Description;
            LblAddress.Text = hairSalon.Location.Address;
            LblCity.Text = hairSalon.Location.City.Name;
            LblCountry.Text = hairSalon.Location.City.Country.Name;

            Owner owner = await ownerApi.GetOwner(hairSalon.OwnerId);
            LblOwner.Text = $"{ owner.FirstName } { owner.LastName }";
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            await LoadHairSalonToListBox();
        }

        private async Task LoadHairSalonToListBox()
        {
            LbMethodsOfPayment.ItemsSource = await hairSalonMethodsOfPaymentApi.GetHairSalonMethodsOfPayment(hairSalon.Id);
            LbWorkingHours.ItemsSource = await hairSalonWorkingHoursApi.GetHairSalonWorkingHours(hairSalon.Id);
            LbWorkers.ItemsSource = await workerApi.GetHairSalonWorkers(hairSalon.Id);
            LbServices.ItemsSource = await hairSalonServicesApi.GetHairSalonServices(hairSalon.Id);
        }

        private void BtnAddMethodOfPayment_Click(object sender, RoutedEventArgs e)
        {
            MethodOfPaymentManagement methodOfPaymentManagementWindow = new MethodOfPaymentManagement(hairSalon);
            methodOfPaymentManagementWindow.Show();
        }

        private async void BtnRemoveSelectedMethodOfPayment_Click(object sender, RoutedEventArgs e)
        {
            await RemoveHairSalonMethodOfPayment();
        }

        private async Task RemoveHairSalonMethodOfPayment()
        {
            var selectedItem = LbMethodsOfPayment.SelectedItem;

            if (selectedItem is HairSalonMethodsOfPayment hairSalonMethodOfPayment)
            {
                bool success = await hairSalonMethodsOfPaymentApi.RemoveHairSalonMethodOfPayment(hairSalonMethodOfPayment);

                if (success)
                {
                    MessageBox.Show("Successfully removed method of payment!");
                }
                else
                {
                    MessageBox.Show("Fail!");
                }
            }
            else
            {
                MessageBox.Show("You must select method of payment before removing!");
            }
        }

        private void BtnAddWorkingHour_Click(object sender, RoutedEventArgs e)
        {
            WorkingHourManagement workingHourManagementWindow = new WorkingHourManagement(hairSalon);
            workingHourManagementWindow.Show();
        }

        private async void BtnRemoveSelectedWorkingHour_Click(object sender, RoutedEventArgs e)
        {
            await RemoveHairSalonWorkingHour();
        }

        private async Task RemoveHairSalonWorkingHour()
        {
            var selectedItem = LbWorkingHours.SelectedItem;

            if (selectedItem is HairSalonWorkingHours hairSalonWorkingHour)
            {
                bool success = await hairSalonWorkingHoursApi.RemoveHairSalonWorkingHour(hairSalonWorkingHour);

                if (success)
                {
                    MessageBox.Show("Successfully removed working hour!");
                }
                else
                {
                    MessageBox.Show("Fail!");
                }
            }
            else
            {
                MessageBox.Show("You must select working hour before removing!");
            }
        }

        private void BtnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            WorkerSelection workerSelectionWindow = new WorkerSelection(hairSalon);
            workerSelectionWindow.Show();
        }

        private async void BtnRemoveSelectedWorker_Click(object sender, RoutedEventArgs e)
        {
            await RemoveHairSalonWorker();
        }

        private async Task RemoveHairSalonWorker()
        {
            var selectedItem = LbWorkers.SelectedItem;

            if (selectedItem is Worker worker)
            {
                bool success = await workerApi.RemoveHairSalonWorker(worker);

                if (success)
                {
                    MessageBox.Show("Successfully removed worker!");
                }
                else
                {
                    MessageBox.Show("Fail!");
                }
            }
            else
            {
                MessageBox.Show("You must select worker before removing!");
            }
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            ServiceManagement serviceManagementWindow = new ServiceManagement(hairSalon);
            serviceManagementWindow.Show();
        }

        private async void BtnRemoveSelectedService_Click(object sender, RoutedEventArgs e)
        {
            await RemoveHairSalonService();
        }

        private async Task RemoveHairSalonService()
        {
            var selectedItem = LbServices.SelectedItem;

            if (selectedItem is HairSalonServices hairSalonService)
            {
                bool success = await hairSalonServicesApi.RemoveHairSalonService(hairSalonService);

                if (success)
                {
                    MessageBox.Show("Successfully removed service!");
                }
                else
                {
                    MessageBox.Show("Fail!");
                }
            }
            else
            {
                MessageBox.Show("You must select service before removing!");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

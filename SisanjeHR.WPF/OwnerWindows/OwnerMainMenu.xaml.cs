using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.ManagementWindows;
using SisanjeHrDesktopApp.SharedWindows;
using Utilities.ViewModels.OwnerViewModels;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Collections.Generic;
using SisanjeHrDesktopApp.WorkerWindows.WorkerCustom;

namespace SisanjeHrDesktopApp.OwnerWindows
{
    /// <summary>
    /// Interaction logic for OwnerMainMenu.xaml
    /// </summary>
    public partial class OwnerMainMenu : Window
    {
        private readonly OwnerApiClient ownerApi;
        private readonly AppointmentApiClient appointmentApi;
        private readonly HairSalonApiClient hairSalonApi;

        private Owner owner;

        public OwnerMainMenu()
        {
            InitializeComponent();

            ownerApi = new OwnerApiClient();
            appointmentApi = new AppointmentApiClient();
            hairSalonApi = new HairSalonApiClient();
        }

        private async void Window_Loaded(object sender, EventArgs e)
        {
            await LoadHairSalonsToComboBox();
        }

        private async Task LoadHairSalonsToComboBox()
        {
            CbHairSalons.ItemsSource = await hairSalonApi.GetHairSalons(owner.Id);
        }

        private void CbHairSalons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpAppointments.Children.Clear();
        }

        public async Task SetOwnerLoggedIn(OwnerLoginVM ownerLoginVM)
        {
            Owner owner = await ownerApi.GetOwnerByUidPwd(ownerLoginVM);
            this.owner = owner;

            LblFirstName.Text = owner.FirstName;
            LblLastName.Text = owner.LastName;
        }

        private void BtnHairSalonManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(new HairSalonManagement(owner));
        }

        private void BtnWorkersManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow(new WorkerManagement(owner));
        }

        private void OpenNewWindow(Window window)
        {
            window.Show();
        }

        private async void CalendarAppointments_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbHairSalons.SelectedItem != null)
            {
                await LoadAppointmentsToStackPanel(sender);
            }
        }

        private async Task LoadAppointmentsToStackPanel(object sender)
        {
            SpAppointments.Children.Clear();

            HairSalon hairSalon = (HairSalon)CbHairSalons.SelectedItem;

            List<Appointment> appointments = await appointmentApi.GetAppointmentsByHairSalon(hairSalon.Id);

            var calendar = sender as Calendar;

            if (appointments.Count > 0)
            {
                foreach (Appointment appointment in appointments)
                {
                    if (calendar.SelectedDate.HasValue && calendar.SelectedDate.Value == appointment.Date)
                    {
                        AppointmentPanelMini appointmentPanelMini = new AppointmentPanelMini(appointment);

                        SpAppointments.Children.Add(appointmentPanelMini);
                    }
                }
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Start startWindow = new Start();
            startWindow.Show();

            Close();
        }
    }
}

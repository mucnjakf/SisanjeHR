using ApiClient.Api;
using DataAccessLayer.Models;
using SisanjeHrDesktopApp.AdminWindows.AddNewWindows;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using SisanjeHrDesktopApp.WorkerWindows.WorkerCustom;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Utilities.ViewModels.AppointmentViewModels;
using Utilities.ViewModels.RegisteredUserViewModels;

namespace SisanjeHrDesktopApp.WorkerWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewAppointment.xaml
    /// </summary>
    public partial class AddNewAppointment : Window
    {
        private readonly LoadingScreen ls;

        private readonly HairSalonServicesApiClient hairSalonServicesApi;
        private readonly AppointmentApiClient appointmentApi;
        private readonly RegisteredUserApiClient registeredUserApi;
        private readonly HairSalonMethodsOfPaymentApiClient hairSalonMethodsOfPaymentApi;

        private readonly Worker worker;

        public AddNewAppointment(Worker worker)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            hairSalonServicesApi = new HairSalonServicesApiClient();
            appointmentApi = new AppointmentApiClient();
            registeredUserApi = new RegisteredUserApiClient();
            hairSalonMethodsOfPaymentApi = new HairSalonMethodsOfPaymentApiClient();

            this.worker = worker;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadServicesToComboBox();

            DpDate.SelectedDate = DateTime.Today;
            TpTime.DefaultValue = DateTime.Now;
            TpTime.Text = TpTime.DefaultValue.ToString();
        }

        private async Task LoadMethodsOfPaymentToCombobox()
        {
            List<MethodOfPayment> methodsOfPayment = new List<MethodOfPayment>();
            List<HairSalonMethodsOfPayment> hairSalonMethodsOfPayments = await hairSalonMethodsOfPaymentApi.GetHairSalonMethodsOfPayment(worker.HairSalonId);

            foreach (var hairSalonMop in hairSalonMethodsOfPayments)
            {
                methodsOfPayment.Add(hairSalonMop.MethodOfPayment);
            }

            CbMethodsOfPayment.ItemsSource = methodsOfPayment;
        }

        private async Task LoadRegisteredUsersToComboBox()
        {
            List<RegisteredUser> registeredUsers = await registeredUserApi.GetRegisteredUsers();
            CbCustomers.ItemsSource = registeredUsers;
        }

        private async Task LoadServicesToComboBox()
        {
            List<Service> services = new List<Service>();
            List<HairSalonServices> hairSalonServices = await hairSalonServicesApi.GetHairSalonServices(worker.HairSalonId);

            foreach (HairSalonServices hairSalonService in hairSalonServices)
            {
                services.Add(hairSalonService.Service);
            }

            CbServices.ItemsSource = services;
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            AddServiceToListBox();
        }

        private void AddServiceToListBox()
        {
            var selectedItem = CbServices.SelectedItem;

            if (selectedItem is Service service)
            {
                if (!LbServices.Items.Contains(service))
                {
                    LbServices.Items.Add(service);
                }
                else
                {
                    MessageBox.Show("This service has already been added!");
                }

            }
        }

        private void BtnRemoveService_Click(object sender, RoutedEventArgs e)
        {
            RemoveServiceFromListBox();
        }

        private void RemoveServiceFromListBox()
        {
            var selectedItem = LbServices.SelectedItem;

            if (selectedItem is Service service)
            {
                LbServices.Items.Remove(service);
            }
            else
            {
                MessageBox.Show("You must select service before removing!");
            }
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertAppointment();
        }

        private async Task InsertAppointment()
        {
            if (ValidateInputs())
            {
                List<Service> services = new List<Service>();

                foreach (Service service in LbServices.Items)
                {
                    services.Add(service);
                }

                AppointmentInsertVM appointmentInsertVM = new AppointmentInsertVM()
                {
                    Date = DpDate.SelectedDate.Value,
                    Time = TpTime.Value.Value.TimeOfDay,
                    RegisteredUser = (RegisteredUser)CbCustomers.SelectedItem,
                    Services = services,
                    HairSalonId = worker.HairSalonId,
                    Worker = worker,
                    MethodOfPayment = (MethodOfPayment)CbMethodsOfPayment.SelectedItem
                };

                bool appointmentIsNotAvailable = await appointmentApi.CheckAppointmentAvailability(new CheckAvailabilityVM() { Date = appointmentInsertVM.Date, Time = appointmentInsertVM.Time, WorkerId = appointmentInsertVM.Worker.Id });
                
                if (appointmentIsNotAvailable)
                {
                    MessageBox.Show("Appointment is not available!");
                    return;
                }

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await appointmentApi.InsertAppointment(appointmentInsertVM);
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
                MessageBox.Show(
                    "All input fields are required!\n" +
                    "You must select atleast one service!");
            }
        }

        private bool ValidateInputs()
        {
            if (DpDate.SelectedDate.Value != null &&
            TpTime.Value != null &&
            CbCustomers.SelectedItem != null &&
            CbMethodsOfPayment.SelectedItem != null &&
            LbServices.Items.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddNewRegisteredUser addNewRegisteredUser = new AddNewRegisteredUser();
            addNewRegisteredUser.Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            await LoadMethodsOfPaymentToCombobox();
            await LoadRegisteredUsersToComboBox();
        }
    }
}

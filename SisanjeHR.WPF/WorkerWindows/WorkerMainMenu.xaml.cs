using ApiClient.Api;
using DataAccessLayer.Models;
using SisanjeHrDesktopApp.SharedWindows;
using SisanjeHrDesktopApp.WorkerWindows.AddNewWindows;
using SisanjeHrDesktopApp.WorkerWindows.WorkerCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Utilities.ViewModels.WorkerViewModels;

namespace SisanjeHrDesktopApp.WorkerWindows
{
    /// <summary>
    /// Interaction logic for WorkerMainMenu.xaml
    /// </summary>
    public partial class WorkerMainMenu : Window
    {
        private readonly WorkerApiClient workerApi;
        private readonly AppointmentApiClient appointmentApi;

        private Worker worker;

        public WorkerMainMenu()
        {
            InitializeComponent();

            workerApi = new WorkerApiClient();
            appointmentApi = new AppointmentApiClient();
        }

        public async Task SetWorkerLoggedIn(WorkerLoginVM workerLoginVM)
        {
            Worker worker = await workerApi.GetWorkerByUidPwd(workerLoginVM);

            LblFirstName.Text = worker.FirstName;
            LblLastName.Text = worker.LastName;
            LblHairSalon.Text = worker.HairSalon.Name;

            this.worker = worker;
        }

        private void BtnAddNewAppointment_Click(object sender, RoutedEventArgs e)
        {
            AddNewAppointment addNewAppointmentWindow = new AddNewAppointment(worker);
            addNewAppointmentWindow.Show();
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            await LoadAppointmentsToStackPanel(CalendarAppointments);
        }

        private async void CalendarAppointments_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            await LoadAppointmentsToStackPanel(sender);
        }

        private async Task LoadAppointmentsToStackPanel(object sender)
        {
            SpAppointments.Children.Clear();

            List<Appointment> appointments = await appointmentApi.GetAppointmentsByWorker(worker.Id);

            var calendar = sender as Calendar;

            if (appointments.Count > 0)
            {
                foreach (Appointment appointment in appointments)
                {
                    if (calendar.SelectedDate.HasValue && calendar.SelectedDate.Value == appointment.Date)
                    {
                        AppointmentPanel appointmentPanel = new AppointmentPanel(appointment);

                        SpAppointments.Children.Add(appointmentPanel);
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

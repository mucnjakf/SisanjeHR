using ApiClient.Api;
using DataAccessLayer.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Utilities.ViewModels.AppointmentViewModels;

namespace SisanjeHrDesktopApp.WorkerWindows.WorkerCustom
{
    /// <summary>
    /// Interaction logic for AppointmentPanel.xaml
    /// </summary>
    public partial class AppointmentPanel : UserControl
    {
        private readonly AppointmentApiClient appointmentApi;

        private readonly Appointment appointment;

        public AppointmentPanel(Appointment appointment)
        {
            InitializeComponent();

            appointmentApi = new AppointmentApiClient();

            this.appointment = appointment;

            SetAppointmentValues();
        }

        private void SetAppointmentValues()
        {
            LblTime.Text = appointment.Time.ToString("hh':'mm':'ss");
            LblCustomer.Text = $"{appointment.RegisteredUser.FirstName} {appointment.RegisteredUser.LastName}";
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowAppointment showAppointmentWindow = new ShowAppointment(appointment);
            showAppointmentWindow.Show();
        }

        private async void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            await CompleteAppointment();
        }

        private async Task CompleteAppointment()
        {
            AppointmentUpdateVM appointmentUpdateVM = new AppointmentUpdateVM()
            {
                IsCompleted = true
            };

            bool success = await appointmentApi.UpdateAppointment(appointment.Id, appointmentUpdateVM);

            if (success)
            {
                MessageBox.Show($"Sucessfully completed appointment!");
            }
            else
            {
                MessageBox.Show("Fail");
            }
        }
    }
}

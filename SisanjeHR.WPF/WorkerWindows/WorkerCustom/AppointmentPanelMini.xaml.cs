using DataAccessLayer.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.WorkerWindows.WorkerCustom
{
    /// <summary>
    /// Interaction logic for AppointmentPanelMini.xaml
    /// </summary>
    public partial class AppointmentPanelMini : UserControl
    {
        private readonly Appointment appointment;

        public AppointmentPanelMini(Appointment appointment)
        {
            InitializeComponent();

            this.appointment = appointment;

            SetAppointmentValues();
        }

        private void SetAppointmentValues()
        {
            LblTime.Text = appointment.Time.ToString(@"hh\:mm\:ss");
            LblWorker.Text = $"{appointment.Worker.FirstName} {appointment.Worker.LastName}";
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowAppointment showAppointmentWindow = new ShowAppointment(appointment);
            showAppointmentWindow.Show();
        }
    }
}

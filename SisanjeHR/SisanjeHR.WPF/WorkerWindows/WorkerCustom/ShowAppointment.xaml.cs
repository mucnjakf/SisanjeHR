using ApiClient.Api;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.WorkerWindows.WorkerCustom
{
    /// <summary>
    /// Interaction logic for ShowAppointment.xaml
    /// </summary>
    public partial class ShowAppointment : Window
    {
        private readonly AppointmentServicesApiClient appointmentServicesApi;

        private readonly Appointment appointment;

        public ShowAppointment(Appointment appointment)
        {
            InitializeComponent();

            appointmentServicesApi = new AppointmentServicesApiClient();

            this.appointment = appointment;

            SetAppointmentValues();
        }

        private async void SetAppointmentValues()
        {
            LblId.Text = appointment.Id.ToString();
            LblDate.Text = appointment.Date.ToString("dd/MM/yyyy");
            LblTime.Text = appointment.Time.ToString("hh':'mm':'ss");
            LblCustomer.Text = $"{appointment.RegisteredUser.FirstName} {appointment.RegisteredUser.LastName}";
            LblPayment.Text = appointment.MethodOfPayment.ToString();
            LblHairSalon.Text = appointment.HairSalon.Name;
            LblWorker.Text = $"{appointment.Worker.FirstName} {appointment.Worker.LastName}";
            LbServices.ItemsSource = await appointmentServicesApi.GetAppointmentServices(appointment.Id);
            LblTotalPrice.Text = $"Total price: {appointment.TotalPrice}";
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

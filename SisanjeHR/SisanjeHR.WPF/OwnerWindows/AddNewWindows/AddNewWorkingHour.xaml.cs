using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.WorkingHourViewModels;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewWorkingHour.xaml
    /// </summary>
    public partial class AddNewWorkingHour : Window
    {
        private readonly LoadingScreen ls;

        private readonly WorkingHourApiClient workingHourApi;

        public AddNewWorkingHour()
        {
            InitializeComponent();

            ls = new LoadingScreen();

            workingHourApi = new WorkingHourApiClient();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TpTimeStart.DefaultValue = DateTime.Now;
            TpTimeStart.Text = TpTimeStart.DefaultValue.ToString();

            TpTimeEnd.DefaultValue = DateTime.Now;
            TpTimeEnd.Text = TpTimeEnd.DefaultValue.ToString();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertWorkingHour();
        }

        private async Task InsertWorkingHour()
        {
            if (ValidateInputs())
            {
                WorkingHourInsertVM workingHourInsertVM = new WorkingHourInsertVM()
                {
                    DayInWeek = TbDayInWeek.Text,
                    TimeStart = TpTimeStart.Value.Value.TimeOfDay,
                    TimeEnd = TpTimeEnd.Value.Value.TimeOfDay
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await workingHourApi.InsertWorkingHour(workingHourInsertVM);
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
                MessageBox.Show("All input fields are required!");
            }
        }

        private bool ValidateInputs()
        {
            if (TbDayInWeek.Text != string.Empty &&
                TpTimeStart.Value != null &&
                TpTimeEnd.Value != null)
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

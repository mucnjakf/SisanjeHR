using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.WorkingHourViewModels;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditWorkingHour.xaml
    /// </summary>
    public partial class EditWorkingHour : Window
    {
        private readonly LoadingScreen ls;

        private readonly WorkingHourApiClient workingHourApi;

        private readonly WorkingHour workingHour;

        public EditWorkingHour(WorkingHour workingHour)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            workingHourApi = new WorkingHourApiClient();

            this.workingHour = workingHour;

            SetWorkingHourValues();
        }

        private void SetWorkingHourValues()
        {
            TbDayInWeek.Text = workingHour.DayInWeek;
            TpTimeStart.Text = workingHour.TimeStart.ToString("hh':'mm':'ss");
            TpTimeEnd.Text = workingHour.TimeEnd.ToString("hh':'mm':'ss");
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateWorkingHour();
        }

        private async Task UpdateWorkingHour()
        {
            if (ValidateInputs())
            {
                WorkingHourEditVM workingHourEditVM = new WorkingHourEditVM()
                {
                    Id = workingHour.Id,
                    DayInWeek = TbDayInWeek.Text,
                    TimeStart = TpTimeStart.Value.Value.TimeOfDay,
                    TimeEnd = TpTimeEnd.Value.Value.TimeOfDay
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await workingHourApi.UpdateWorkingHour(workingHourEditVM);
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

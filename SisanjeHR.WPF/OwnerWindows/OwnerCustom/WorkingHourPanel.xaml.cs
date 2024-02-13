using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.EditWindows;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SisanjeHrDesktopApp.OwnerWindows.OwnerCustom
{
    /// <summary>
    /// Interaction logic for WorkingHourPanel.xaml
    /// </summary>
    public partial class WorkingHourPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly WorkingHourApiClient workingHourApi;
        private readonly HairSalonWorkingHoursApiClient hairSalonWorkingHoursApi;

        private readonly WorkingHour workingHour;
        private readonly HairSalon hairSalon;

        public WorkingHourPanel(WorkingHour workingHour, HairSalon hairSalon)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            workingHourApi = new WorkingHourApiClient();
            hairSalonWorkingHoursApi = new HairSalonWorkingHoursApiClient();

            this.workingHour = workingHour;
            this.hairSalon = hairSalon;

            SetWorkingHourValues();
        }

        private void SetWorkingHourValues()
        {
            LblDayInWeek.Text = workingHour.DayInWeek;
            LblTimeStart.Text = workingHour.TimeStart.ToString("hh':'mm':'ss");
            LblTimeEnd.Text = workingHour.TimeEnd.ToString("hh':'mm':'ss");
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            await AddWorkingHourToHairSalon();
        }

        private async Task AddWorkingHourToHairSalon()
        {
            bool success = await hairSalonWorkingHoursApi.AddWorkingHourToHairSalon(hairSalon.Id, workingHour);

            if (success)
            {
                MessageBox.Show("Successfully added working hour!");
            }
            else
            {
                MessageBox.Show("Hair Salon already contains this working hour!");
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditWorkingHour editWorkingHourWindow = new EditWorkingHour(workingHour);
            editWorkingHourWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteWorkingHour();
        }

        private async Task DeleteWorkingHour()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await workingHourApi.DeleteWorkingHour(workingHour.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail!");
            }
        }
    }
}

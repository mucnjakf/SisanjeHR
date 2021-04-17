using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.AddNewWindows;
using SisanjeHrDesktopApp.OwnerWindows.OwnerCustom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.ManagementWindows
{
    /// <summary>
    /// Interaction logic for WorkingHourManagement.xaml
    /// </summary>
    public partial class WorkingHourManagement : Window
    {
        private readonly WorkingHourApiClient workingHourApi;

        private readonly HairSalon hairSalon;

        public WorkingHourManagement(HairSalon hairSalon)
        {
            InitializeComponent();

            workingHourApi = new WorkingHourApiClient();

            this.hairSalon = hairSalon;
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbWorkingHours.IsIndeterminate = true;
            await LoadWorkingHoursToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbWorkingHours.Visibility = Visibility.Hidden;
        }

        private async Task LoadWorkingHoursToStackPanel()
        {
            SpWorkingHours.Children.Clear();

            List<WorkingHour> workingHours = await workingHourApi.GetWorkingHours();

            if (workingHours.Count > 0)
            {
                LblNoWorkingHours.Visibility = Visibility.Hidden;

                foreach (WorkingHour workingHour in workingHours)
                {
                    WorkingHourPanel workingHourPanel = new WorkingHourPanel(workingHour, hairSalon);

                    SpWorkingHours.Children.Add(workingHourPanel);
                }
            }
            else
            {
                LblNoWorkingHours.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddNewWorkingHour_Click(object sender, RoutedEventArgs e)
        {
            AddNewWorkingHour addNewWorkingHour = new AddNewWorkingHour();
            addNewWorkingHour.Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

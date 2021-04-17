using DataAccessLayer.Models;
using ApiClient.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.OwnerCustom
{
    /// <summary>
    /// Interaction logic for WorkerSelection.xaml
    /// </summary>
    public partial class WorkerSelection : Window
    {
        private readonly WorkerApiClient workerApi;

        private readonly HairSalon hairSalon;

        public WorkerSelection(HairSalon hairSalon)
        {
            InitializeComponent();

            workerApi = new WorkerApiClient();

            this.hairSalon = hairSalon;
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbWorkers.IsIndeterminate = true;
            await LoadWorkersToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbWorkers.Visibility = Visibility.Hidden;
        }

        private async Task LoadWorkersToStackPanel()
        {
            SpWorkers.Children.Clear();

            List<Worker> workers = await workerApi.GetWorkers(hairSalon.OwnerId);

            if (workers.Count > 0)
            {
                LblNoWorkers.Visibility = Visibility.Hidden;

                foreach (Worker worker in workers)
                {
                    WorkerSelectionPanel workerSelectionPanel = new WorkerSelectionPanel(worker, hairSalon);

                    SpWorkers.Children.Add(workerSelectionPanel);
                }
            }
            else
            {
                LblNoWorkers.Visibility = Visibility.Visible;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

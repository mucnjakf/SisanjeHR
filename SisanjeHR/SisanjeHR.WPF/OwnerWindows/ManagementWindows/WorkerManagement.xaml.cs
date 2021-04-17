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
    /// Interaction logic for WorkerManagement.xaml
    /// </summary>
    public partial class WorkerManagement : Window
    {
        private readonly WorkerApiClient workerApi;

        private readonly Owner owner;

        public WorkerManagement(Owner owner)
        {
            InitializeComponent();

            workerApi = new WorkerApiClient();

            this.owner = owner;
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

            List<Worker> workers = await workerApi.GetWorkers(owner.Id);

            if (workers.Count > 0)
            {
                foreach (Worker worker in workers)
                {
                    LblNoWorkers.Visibility = Visibility.Hidden;

                    WorkerPanel workerPanel = new WorkerPanel(worker);
                    SpWorkers.Children.Add(workerPanel);
                }
            }
            else
            {
                LblNoWorkers.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddNewWorker_Click(object sender, RoutedEventArgs e)
        {
            AddNewWorker addNewWorkerWindow = new AddNewWorker(owner);
            addNewWorkerWindow.Show();
        }

        private void BtnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

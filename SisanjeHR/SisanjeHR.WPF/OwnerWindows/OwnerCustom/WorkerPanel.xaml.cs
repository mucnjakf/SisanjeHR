using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.EditWindows;
using SisanjeHrDesktopApp.OwnerWindows.ShowWindows;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.OwnerWindows.OwnerCustom
{
    /// <summary>
    /// Interaction logic for WorkerPanel.xaml
    /// </summary>
    public partial class WorkerPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly WorkerApiClient workerApi;

        private readonly Worker worker;

        public WorkerPanel(Worker worker)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            workerApi = new WorkerApiClient();

            this.worker = worker;

            SetWorkerValues();
        }

        private void SetWorkerValues()
        {
            LblFirstName.Text = worker.FirstName;
            LblLastName.Text = worker.LastName;
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowWorker showWorkerWindow = new ShowWorker(worker);
            showWorkerWindow.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditWorker editWorkerWindow = new EditWorker(worker);
            editWorkerWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteWorker();
        }

        private async Task DeleteWorker()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await workerApi.DeleteWorker(worker.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail");
            }
        }
    }
}

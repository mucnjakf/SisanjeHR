using DataAccessLayer.Models;
using ApiClient.Api;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.OwnerWindows.OwnerCustom
{
    /// <summary>
    /// Interaction logic for WorkerSelectionPanel.xaml
    /// </summary>
    public partial class WorkerSelectionPanel : UserControl
    {
        private readonly WorkerApiClient workerApi;

        private readonly Worker worker;
        private readonly HairSalon hairSalon;

        public WorkerSelectionPanel(Worker worker, HairSalon hairSalon)
        {
            InitializeComponent();

            workerApi = new WorkerApiClient();

            this.worker = worker;
            this.hairSalon = hairSalon;

            SetWorkerValues();
        }

        private void SetWorkerValues()
        {
            LblFirstName.Text = worker.FirstName;
            LblLastName.Text = worker.LastName;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            await AddWorkerToHairSalon();
        }

        private async Task AddWorkerToHairSalon()
        {
            bool success = await workerApi.AddWorkerToHairSalon(hairSalon.Id, worker);

            if (success)
            {
                MessageBox.Show("Successfully added worker!");
            }
            else
            {
                MessageBox.Show("Worker is already employed!");
            }
        }
    }
}

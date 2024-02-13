using DataAccessLayer.Models;
using SisanjeHrDesktopApp.AdminWindows.EditWindows;
using SisanjeHrDesktopApp.AdminWindows.ShowWindows;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.AdminWindows.AdminCustom
{
    /// <summary>
    /// Interaction logic for OwnerPanel.xaml
    /// </summary>
    public partial class OwnerPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly OwnerApiClient ownerApi;

        private readonly Owner owner;

        public OwnerPanel(Owner owner)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            ownerApi = new OwnerApiClient();

            this.owner = owner;

            SetOwnerValues();
        }

        private void SetOwnerValues()
        {
            LblFirstName.Text = owner.FirstName;
            LblLastName.Text = owner.LastName;
            LblSubscription.Text = owner.Subscription.Type;
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowOwner showOwnerWindow = new ShowOwner(owner);
            showOwnerWindow.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditOwner editOwnerWindow = new EditOwner(owner);
            editOwnerWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteOwner();
        }

        private async Task DeleteOwner()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await ownerApi.DeleteOwner(owner.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail!");
            }
        }
    }
}

using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows.EditWindows;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SisanjeHrDesktopApp.OwnerWindows.OwnerCustom
{
    /// <summary>
    /// Interaction logic for MethodOfPaymentPanel.xaml
    /// </summary>
    public partial class MethodOfPaymentPanel : UserControl
    {
        private readonly LoadingScreen ls;

        private readonly MethodOfPaymentApiClient methodOfPaymentApi;
        private readonly HairSalonMethodsOfPaymentApiClient hairSalonMethodsOfPaymentApi;

        private readonly MethodOfPayment methodOfPayment;
        private readonly HairSalon hairSalon;

        public MethodOfPaymentPanel(MethodOfPayment methodOfPayment, HairSalon hairSalon)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            methodOfPaymentApi = new MethodOfPaymentApiClient();
            hairSalonMethodsOfPaymentApi = new HairSalonMethodsOfPaymentApiClient();

            this.methodOfPayment = methodOfPayment;
            this.hairSalon = hairSalon;

            SetMethodOfPaymentValues();
        }

        private void SetMethodOfPaymentValues()
        {
            LblMethod.Text = methodOfPayment.Method;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            await AddMethodOfPaymentToHairSalon();
        }

        private async Task AddMethodOfPaymentToHairSalon()
        {
            bool success = await hairSalonMethodsOfPaymentApi.AddMethodOfPaymentToHairSalon(hairSalon.Id, methodOfPayment);

            if (success)
            {
                MessageBox.Show("Successfully added method of payment!");
            }
            else
            {
                MessageBox.Show("Hair Salon already contains this method of payment!");
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditMethodOfPayment editMethodOfPaymentWindow = new EditMethodOfPayment(methodOfPayment);
            editMethodOfPaymentWindow.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await DeleteMethodOfPayment();
        }

        private async Task DeleteMethodOfPayment()
        {
            ls.LblLoading.Text = "Deleting";
            ls.Show();
            bool success = await methodOfPaymentApi.DeleteMethodOfPayment(methodOfPayment.Id);
            ls.Close();

            if (success == false)
            {
                MessageBox.Show("Fail!");
            }
        }
    }
}

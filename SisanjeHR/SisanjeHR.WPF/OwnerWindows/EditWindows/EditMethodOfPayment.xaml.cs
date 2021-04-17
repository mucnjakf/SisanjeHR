using DataAccessLayer.Models;
using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.MethodOfPaymentViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.EditWindows
{
    /// <summary>
    /// Interaction logic for EditMethodOfPayment.xaml
    /// </summary>
    public partial class EditMethodOfPayment : Window
    {
        private readonly LoadingScreen ls;

        private readonly MethodOfPaymentApiClient methodOfPaymentApi;

        private readonly MethodOfPayment methodOfPayment;

        public EditMethodOfPayment(MethodOfPayment methodOfPayment)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            methodOfPaymentApi = new MethodOfPaymentApiClient();

            this.methodOfPayment = methodOfPayment;

            SetMethodOfPaymentValues();
        }

        private void SetMethodOfPaymentValues()
        {
            TbMethod.Text = methodOfPayment.Method;
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await UpdateMethodOfPayment();
        }

        private async Task UpdateMethodOfPayment()
        {
            if (ValidateInputs())
            {
                MethodOfPaymentEditVM methodOfPaymentEditVM = new MethodOfPaymentEditVM()
                {
                    Id = methodOfPayment.Id,
                    Method = TbMethod.Text
                };

                ls.LblLoading.Text = "Editing";
                ls.Show();
                bool success = await methodOfPaymentApi.UpdateMethodOfPayment(methodOfPaymentEditVM);
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
            if (TbMethod.Text != string.Empty)
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

using ApiClient.Api;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.MethodOfPaymentViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.AddNewWindows
{
    /// <summary>
    /// Interaction logic for AddNewMethodOfPayment.xaml
    /// </summary>
    public partial class AddNewMethodOfPayment : Window
    {
        private readonly LoadingScreen ls;

        private readonly MethodOfPaymentApiClient methodOfPaymentApi;

        public AddNewMethodOfPayment()
        {
            InitializeComponent();

            ls = new LoadingScreen();

            methodOfPaymentApi = new MethodOfPaymentApiClient();
        }

        private async void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            await InsertMethodOfPayment();
        }

        private async Task InsertMethodOfPayment()
        {
            if (ValidateInputs())
            {
                MethodOfPaymentInsertVM methodOfPaymentInsertVM = new MethodOfPaymentInsertVM()
                {
                    Method = TbMethod.Text
                };

                ls.LblLoading.Text = "Adding";
                ls.Show();
                bool success = await methodOfPaymentApi.InsertMethodOfPayment(methodOfPaymentInsertVM);
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

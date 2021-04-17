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
    /// Interaction logic for MethodOfPaymentManagement.xaml
    /// </summary>
    public partial class MethodOfPaymentManagement : Window
    {
        private readonly MethodOfPaymentApiClient methodOfPaymentApi;

        private readonly HairSalon hairSalon;

        public MethodOfPaymentManagement(HairSalon hairSalon)
        {
            InitializeComponent();

            methodOfPaymentApi = new MethodOfPaymentApiClient();

            this.hairSalon = hairSalon;
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            LblLoading.Visibility = Visibility.Visible;
            PbMethodsOfPayment.IsIndeterminate = true;
            await LoadMethodsOfPaymentToStackPanel();
            LblLoading.Visibility = Visibility.Hidden;
            PbMethodsOfPayment.Visibility = Visibility.Hidden;
        }

        private async Task LoadMethodsOfPaymentToStackPanel()
        {
            SpMethodsOfPayment.Children.Clear();

            List<MethodOfPayment> methodsOfPayment = await methodOfPaymentApi.GetMethodsOfPayment();

            if (methodsOfPayment.Count > 0)
            {
                LblNoMethodsOfPayment.Visibility = Visibility.Hidden;

                foreach (MethodOfPayment methodOfPayment in methodsOfPayment)
                {
                    MethodOfPaymentPanel methodOfPaymentPanel = new MethodOfPaymentPanel(methodOfPayment, hairSalon);

                    SpMethodsOfPayment.Children.Add(methodOfPaymentPanel);
                }
            }
            else
            {
                LblNoMethodsOfPayment.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddNewMethodOfPayment_Click(object sender, RoutedEventArgs e)
        {
            AddNewMethodOfPayment addNewMethodOfPaymentWindow = new AddNewMethodOfPayment();
            addNewMethodOfPaymentWindow.Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

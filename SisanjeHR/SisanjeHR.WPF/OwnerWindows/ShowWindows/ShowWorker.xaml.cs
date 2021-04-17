using DataAccessLayer.Models;
using System;
using System.Windows;

namespace SisanjeHrDesktopApp.OwnerWindows.ShowWindows
{
    /// <summary>
    /// Interaction logic for ShowWorker.xaml
    /// </summary>
    public partial class ShowWorker : Window
    {
        private readonly Worker worker;

        public ShowWorker(Worker worker)
        {
            InitializeComponent();

            this.worker = worker;

            SetWorkerValues();
        }

        private void SetWorkerValues()
        {
            LblId.Text = worker.Id.ToString();
            LblFirstName.Text = worker.FirstName;
            LblLastName.Text = worker.LastName;
            LblPhoneNumber.Text = worker.PhoneNumber;
            LblUsername.Text = worker.Username;
            LblHairSalon.Text = worker.HairSalon != null ? worker.HairSalon.Name : "Unemployeed";
            LblOwner.Text = $"{worker.Owner.FirstName} {worker.Owner.LastName}";            
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
